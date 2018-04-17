using ridetimes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;


namespace ridetimes.Helper
{
    public class Database
    {
        public static void Insert(List<Models.Entry> rides, int parkID, DateTime stamp)
        {
            //var stamp = DateTime.Now;
            InsertRideStatus(rides, stamp);
            InsertRideTime(rides, parkID, stamp);
        }

        public static void InsertRide(Entry ride, int parkID)
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            //DateTime timeStamp = DateTime.Now;

            using (SqlConnection openCon = new SqlConnection(connection))
            {
                string insertRide = "INSERT into Ride (Name,TypeID,ParkID) VALUES (@Name,@TypeID,@ParkID)";

                using (SqlCommand queryInsert = new SqlCommand(insertRide))
                {
                    queryInsert.Connection = openCon;
                    //queryInsertRide.Parameters.Add("@RideID", SqlDbType.Int, 32).Value = ride.id;
                    queryInsert.Parameters.Add("@Name", SqlDbType.NChar, 100).Value = ride.name;

                    queryInsert.Parameters.Add("@TypeID", SqlDbType.Int, 32).Value = GetRideTypeID(ride.type);
                    queryInsert.Parameters.Add("@ParkID", SqlDbType.Int, 32).Value = parkID;
                    //openCon.Open();

                    try
                    {
                        openCon.Open();
                        int recordsAffected = queryInsert.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        openCon.Close();
                    }
                }
            }
        }
        public static void InsertRideStatus(List<Models.Entry> rides, DateTime stamp)
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection openCon = new SqlConnection(connection))
            {
                foreach (var ride in rides)
                {
                    string insertRide = "INSERT into RideStatusHistory (RideID,RideStatusID,DateTime) VALUES (@RideID,@RideStatusID,@DateTime)";

                    using (SqlCommand queryInsert = new SqlCommand(insertRide))
                    {
                        queryInsert.Connection = openCon;
                        //queryInsertRide.Parameters.Add("@RideID", SqlDbType.Int, 32).Value = ride.id;
                        queryInsert.Parameters.Add("@RideID", SqlDbType.Int, 32).Value = FindID("RideID", "Ride", "Name", ride.name);

                        queryInsert.Parameters.Add("@RideStatusID", SqlDbType.Int, 32).Value = FindID("RideStatusID", "RideStatus", "Status", ride.waitTime.status);
                        queryInsert.Parameters.Add("@DateTime", SqlDbType.DateTime, 32).Value = stamp;
                        //openCon.Open();

                        try
                        {
                            openCon.Open();
                            int recordsAffected = queryInsert.ExecuteNonQuery();
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        finally
                        {
                            openCon.Close();
                        }
                    }
                }
            }
        }
        public static void InsertRideTime(List<Models.Entry> rides,int parkID, DateTime stamp)
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            //DateTime timeStamp = DateTime.Now;

            using (SqlConnection openCon = new SqlConnection(connection))
            {
                foreach (var ride in rides)
                {
                    if (!DoesRideExist("Name", "Ride", "Name", ride.name))
                    {
                        InsertRide(ride, parkID);
                    }

                    string insertRide = "INSERT into RideTimeEntry (RideID,WaitTime,DateTime) VALUES (@RideID,@WaitTime,@DateTime)";

                    using (SqlCommand queryInsertRide = new SqlCommand(insertRide))
                    {
                        queryInsertRide.Connection = openCon;
                        //queryInsertRide.Parameters.Add("@RideID", SqlDbType.Int, 32).Value = ride.id;
                        queryInsertRide.Parameters.Add("@RideID", SqlDbType.Int, 32).Value = FindID("RideID","Ride","Name", ride.name);//ride.name;
                        
                        queryInsertRide.Parameters.Add("@WaitTime", SqlDbType.Int, 32).Value = ride.waitTime.postedWaitMinutes;
                        queryInsertRide.Parameters.Add("@DateTime", SqlDbType.DateTime, 32).Value = stamp;
                        //openCon.Open();

                        try
                        {
                            openCon.Open();
                            int recordsAffected = queryInsertRide.ExecuteNonQuery();
                        }
                        finally
                        {
                            openCon.Close();
                        }
                    }
                }
            }
        }

        public static int FindID(string id, string table, string parameter, string name)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection openCon = new SqlConnection(connection))
                {
                    string select = $"SELECT ({id}) FROM [{table}] WHERE {parameter}=@Name";

                    using (SqlCommand querySelect = new SqlCommand(select))
                    {
                        querySelect.Connection = openCon;
                        querySelect.Parameters.AddWithValue("@Name", name);
                        try
                        {
                            openCon.Open();
                            using (SqlDataReader reader = querySelect.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    return (int)reader[id];
                                }
                            }
                        }
                        catch(SqlException e)
                        {
                            Console.WriteLine(e);
                        }
                        finally { openCon.Close(); }
                    }
                }
            }
            catch(SqlException e)
            {
                return 0;
            }
            return 0;
        }
        public static bool DoesRideExist(string id, string table, string parameter, string name)
        {
            try
            {
                string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection openCon = new SqlConnection(connection))
                {
                    string select = $"SELECT ({id}) FROM [{table}] WHERE {parameter}=@Name";

                    using (SqlCommand querySelect = new SqlCommand(select))
                    {
                        querySelect.Connection = openCon;
                        querySelect.Parameters.AddWithValue("@Name", name);
                        try
                        {
                            openCon.Open();
                            using (SqlDataReader reader = querySelect.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        if (reader.GetString(0).Trim().Equals(name.Trim()))
                                            return true;
                                        else
                                            return false;
                                    }
                                }
                                else return false;
                            }
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine(e);
                        }
                        finally { openCon.Close(); }
                    }
                }
            }
            catch (SqlException e)
            {
                return false;
            }
            return true;
        }
        public static int GetRideTypeID(string rideType)
        {
            if (rideType.Equals("Attraction"))
                return (int)RideType.Attraction;
            else if (rideType.Equals("Entertainment"))
                return (int)RideType.Entertainment;
            else
                throw new Exception($"{rideType} does not exist");
        }
    }
}