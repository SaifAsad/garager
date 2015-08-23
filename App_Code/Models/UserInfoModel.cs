using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInfoModel
/// </summary>
public class UserInfoModel
{
    public UserInformation GetUserInformation(string guId)
    {
        GarageDBEntities db = new GarageDBEntities();
        var info = (from x in db.UserInformations
                    where x.GUID == guId
                    select x).FirstOrDefault();
        return info;
    }


    //this method will be called whenever a new user is created
    public void InsertUserDetail(UserInformation info)
    {
        GarageDBEntities db = new GarageDBEntities();
        db.UserInformations.Add(info);
        db.SaveChanges();
    }
}