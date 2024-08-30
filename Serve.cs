﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;

namespace AMS
{
    public class Serve
    {
        public DataTable getWebsiteByCampaignId(string spname, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CampaignId", SqlDbType.Int).Value = UserId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getZonesById(string spname, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@WebsiteId", SqlDbType.Int).Value = UserId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getBannerListById(string spname, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getCampaignById(string spname, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getCampaignByCampaignId(string spname, int CampaignId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CampaignId", SqlDbType.Int).Value = CampaignId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getCampaignListById(string spname, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getWebsiteListById(string spname, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getUserDetails(string spname, string email, string password)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public string updateBannerById(string spname, int websiteID, int status, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand(spname, con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@BannerId", SqlDbType.Int).Value = websiteID;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Banner has been updated successfully";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Banner update process unsuccessful";
                    }
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    return ex.Message + " - " + "Banner update process unsuccessful";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Banner update process unsuccessful";
            }
            finally
            {
                con.Close();
            }
        }
        public string updateWebsiteById(string spname, int websiteID, int status, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand(spname, con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@WebsiteId", SqlDbType.Int).Value = websiteID;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Website has been updated successfully";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Website update process unsuccessful";
                    }
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    return ex.Message + " - " + "Website update process unsuccessful";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Website update process unsuccessful";
            }
            finally
            {
                con.Close();
            }
        }
        public string insertBanner(string spname, int WebsiteId, string BannerSize, int CampaignDDLVlu, int ZoneId, string BannerTypeId, string Target, string txtBannerLinkVlu, string txtBannerNameVlu, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("insertBanner", con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Name", SqlDbType.Char).Value = txtBannerNameVlu.Trim();
                cmd.Parameters.Add("@WebsiteId", SqlDbType.Int).Value = WebsiteId;
                cmd.Parameters.Add("@CampaignId", SqlDbType.Int).Value = CampaignDDLVlu;
                cmd.Parameters.Add("@BannerSizeId", SqlDbType.Char).Value = BannerSize;
                cmd.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneId;
                cmd.Parameters.Add("@BannerTypeId", SqlDbType.Char).Value = BannerTypeId.Trim();
                cmd.Parameters.Add("@Target", SqlDbType.Char).Value = Target.Trim();
                cmd.Parameters.Add("@BannerLink", SqlDbType.Char).Value = txtBannerLinkVlu.Trim();
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Banner has been created successfully.";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Banner creation process unsuccessful.";
                    }
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    return ex.Message + " - " + "Operation unsuccessful.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Banner creation process unsuccessful.";
            }
            finally
            {
                con.Close();
            }
        }
        public string insertWebsite(string spname, string Name, string WebsiteUrl, string TargetFrame, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("insertWebsite", con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Name", SqlDbType.Char).Value = Name.Trim();
                cmd.Parameters.Add("@WebsiteUrl", SqlDbType.VarChar).Value = WebsiteUrl.Trim();
                cmd.Parameters.Add("@TargetFrame", SqlDbType.Int).Value = TargetFrame;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Website has been created successfully.";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Website creation process unsuccessful.";
                    }
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    if (ex.Message.Contains("PRIMARY KEY"))
                    {
                        return "Website already exists.";
                    }
                    else
                    {
                        return ex.Message + " - " + "Operation unsuccessful.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Website creation process unsuccessful.";
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getWebsiteByAdvertiserId(string spname, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@AdvertiserId", SqlDbType.Int).Value = UserId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getZoneListById(string spname, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public string updateZoneById(string spname, int ZoneId, int Status, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("updateZoneById", con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneId;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Zone has been updated successfully";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Zone update process unsuccessful";
                    }
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    return ex.Message + " - " + "Zone update process unsuccessful";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Zone update process unsuccessful";
            }
            finally
            {
                con.Close();
            }
        }
        public string insertZone(string spname, string Name, string Description, string WebsiteId, string ZoneTypeId, string ZoneSizeId, int mWidth, int mHeight, int Id)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("insertZone", con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Name", SqlDbType.Char).Value = Name.Trim();
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description.Trim();
                cmd.Parameters.Add("@WebsiteId", SqlDbType.Int).Value = WebsiteId;
                cmd.Parameters.Add("@ZoneTypeId", SqlDbType.Char).Value = ZoneTypeId.Trim();
                cmd.Parameters.Add("@ZoneSizeId", SqlDbType.Char).Value = ZoneSizeId.Trim();
                cmd.Parameters.Add("@mWidth", SqlDbType.Int).Value = mWidth;
                cmd.Parameters.Add("@mHeight", SqlDbType.Int).Value = mHeight;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Id;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Zone has been created successfully.";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Zone creation process unsuccessful.";
                    }
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    if (ex.Message.Contains("PRIMARY KEY"))
                    {
                        return "Zone already exists.";
                    }
                    else
                    {
                        return ex.Message + " - " + "Operation unsuccessful.";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Operation unsuccessful.";
            }
            finally
            {
                con.Close();
            }
        }
        public DataTable getCredentialsByEmail(string spname, string Email)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(spname, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.Int).Value = Email;

                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dr.Close();

                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    DataTable dtx = new DataTable();
                    return dtx;
                }
            }
            catch
            {
                DataTable dtx = new DataTable();
                return dtx;
            }
            finally
            {
                con.Close();
            }
        }
        public string updateCampaignById(string spname, int CampaignId, int status, int UserId)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("updateCampaignById", con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CampaignId", SqlDbType.Int).Value = CampaignId;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = status;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Campaign has been updated successfully";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Campaign update process unsuccessful";
                    }
                }
                catch (Exception ex)
                {
                    trn.Rollback();
                    return ex.Message + " - " + "Campaign update process unsuccessful";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Campaign update process unsuccessful";
            }
            finally
            {
                con.Close();
            }
        }
        public string updateCampaign(string spname, int txtCampaignId, int prio, int Id)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("updateCampaign", con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id", SqlDbType.Char).Value = txtCampaignId;
                cmd.Parameters.Add("@Priority", SqlDbType.Int).Value = prio;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Id;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Campaign has been updated successfully.";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Campaign update process unsuccessful.";
                    }
                }
                catch
                {
                    trn.Rollback();
                    return "Campaign update process unsuccessful.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Campaign update process unsuccessful.";
            }
            finally
            {
                con.Close();
            }
        }
        public string insertCampaign(string spname, string txtCampaignName_, string txtCampaignDescription_, int prio, int AdvertiserDDL_, decimal budget,
            string startDate, string endDate, int Id)
        {
            SqlConnection con = new SqlConnection("Data Source=iq-it.database.windows.net;User ID=azureadmin;Password=Iqit@#@#;Initial Catalog=AMS_BE; Connection Timeout=320;pooling=true;Max Pool Size=400");
            try
            {
                con.Open();
                SqlTransaction trn = con.BeginTransaction();

                SqlCommand cmd = new SqlCommand("insertCampaign", con, trn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Name", SqlDbType.Char).Value = txtCampaignName_;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = txtCampaignDescription_;
                cmd.Parameters.Add("@Priority", SqlDbType.Int).Value = prio;
                cmd.Parameters.Add("@AdvertiserId", SqlDbType.Int).Value = AdvertiserDDL_;
                cmd.Parameters.Add("@Budget", SqlDbType.Decimal).Value = budget;
                cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = Id;

                try
                {
                    int cunt = cmd.ExecuteNonQuery();
                    if (cunt > 0)
                    {
                        trn.Commit();
                        return "Campaign has been created successfully.";
                    }
                    else
                    {
                        trn.Rollback();
                        return "Campaign creation process unsuccessful.";
                    }
                }
                catch
                {
                    trn.Rollback();
                    return "Campaign creation process unsuccessful.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " - " + "Campaign creation process unsuccessful.";
            }
            finally
            {
                con.Close();
            }
        }
    }
}