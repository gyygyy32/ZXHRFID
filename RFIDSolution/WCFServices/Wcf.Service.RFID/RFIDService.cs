//#define MYSQL_LOCALHOST
#define MYSQL_CLOUD
//#define SQLSERVER_CLOUD
//#define SQLSERVER_LOCALHOST
//#define SQLSERVER_TengHui

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wcf.ServiceContracts.RFID;
using System.ServiceModel;

namespace Wcf.Service.RFID
{
    public class RFIDService : IRFIDService
    {

#if MYSQL_LOCALHOST
        private DB_Type m_dbType = DB_Type.MySql;
        private const string m_dbAddress = "192.168.56.1";
        private const string connect_string = "server=" + m_dbAddress + ";uid=mesadmin;pwd=1qAZ2wSX;database=js_mes;";
#endif

#if MYSQL_CLOUD
        private DB_Type m_dbType = DB_Type.MySql;
        private const string m_dbAddress = "localhost";
        private const string connect_string = "server=" + m_dbAddress + ";uid=mesadmin;pwd=1qAZ2wSX;database=js_mes;";
#endif

#if SQLSERVER_LOCALHOST
        private DB_Type m_dbType = DB_Type.SqlServer;
        private const string m_dbAddress = "localhost";
        const string connect_string =
            "Server=localhost;" +
            "Database=mes_level2_iface;" +
            "User id=mes;" +
            "Password=1qAZ2wSX;";
#endif


#if SQLSERVER_TengHui
        private DB_Type m_dbType = DB_Type.SqlServer;
        private const string m_dbAddress = "";
        const string connect_string =
            "Server=172.16.2.23;" +
            "Database=TH_MES;" +
            "User id=sa;" +
            "Password=@dminPROduction;";

#endif

        private const string m_errLogFileName = "rfidErrorLog.txt";

        public void WriteLog(object[] parms)
        { 
            string tagID=(string)parms[0];
            string moduleid = (string)parms[1];
            string basic = (string)parms[2];

            tagID=tagID.Replace(" ","");

            string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

#if SQLSERVER_TengHui
            string sql = @"
insert into dbo.rfid_writedlog(tagid,serial_nbr,create_date,basicbuff,pointbuff)
values('{0}','{1}','{2}','{3}','')
";
            sql = string.Format(sql, tagID, moduleid, createtime, basic);

            
            DBOperation.ExecuteMSSqlServerNonQuery(connect_string, sql);

#endif


#if SQLSERVER_LOCALHOST
            string sql = @"
insert into mes_main.dbo.rfid_writedlog(tagid,serial_nbr,create_date,basicbuff,pointbuff)
values('{0}','{1}','{2}','{3}','')
";
            sql = string.Format(sql, tagID, moduleid, createtime, basic);

            
            DBOperation.ExecuteMSSqlServerNonQuery(connect_string, sql);

#endif

#if MYSQL_CLOUD
            string sql = @"
insert into rt_rfid_writedlog(tagid,ModuleID,CreateTime,basicbuff,pointbuff)
values('{0}','{1}','{2}','{3}','')
";
            sql = string.Format(sql, tagID, moduleid, createtime, basic);


            DBOperation.ExecuteNonQuery(connect_string, sql);

#endif

        }

        public void writeTag()
        { 
            
        }

        public void readTag()
        { 
        }

        public ModuleInfo getModuleInfo(object[] parms)
        {
            string serial=(string)parms[0];
            string uniqueTid = (string)parms[1];

            //check if this tag has writen data before by unique tid
            bool b_writenDataBefore = false;

            Dictionary<string, object> dic = new Dictionary<string, object>();

            string sp_parm_serial_no = "strModuleID";

            string conn = "";
            ModuleInfo mi = null;


#if SQLSERVER_LOCALHOST
            dic.Add(sp_parm_serial_no, serial);

            conn = connect_string;

            IEnumerable<ModuleInfo> miList = DBOperation.GetMSRowsBySP<ModuleInfo>(conn, "Get_RFID_BasicInfo_V2", dic, r =>
            {
                ModuleInfo obj = new ModuleInfo();
                try
                {
                    obj.ProductType = r.IsDBNull(0) ? "" : r[0].ToString();
                    obj.ELGrade = r.IsDBNull(1) ? "" : r[1].ToString();
                    obj.Status = r.IsDBNull(2) ? "" : r[2].ToString();
                    obj.PackedDate = r.IsDBNull(3) ? "" : r[3].ToString();
                    obj.Pmax = r.IsDBNull(4) ? "" : r[4].ToString();
                    obj.Voc = r.IsDBNull(5) ? "" : r[5].ToString();
                    obj.Isc = r.IsDBNull(6) ? "" : r[6].ToString();
                    obj.Vpm = r.IsDBNull(7) ? "" : r[7].ToString();
                    obj.Ipm = r.IsDBNull(8) ? "" : r[8].ToString();
                    obj.FF = r.IsDBNull(9) ? "" : r[9].ToString();
                    obj.Pivf = r.IsDBNull(10) ? "" : r[10].ToString();
                    obj.Module_ID = r.IsDBNull(11) ? "" : r[11].ToString();
                    obj.PalletNO = r.IsDBNull(12) ? "" : r[12].ToString();
                    obj.CellDate = r.IsDBNull(13) ? "" : r[13].ToString();
                    obj.Cellsource = r.IsDBNull(14) ? "" : r[14].ToString();
                    obj.EqpID = r.IsDBNull(15) ? "" : r[15].ToString();
                    obj.IVFilePath = r.IsDBNull(16) ? "" : r[16].ToString();
                    obj.cell_supplier_country = r.IsDBNull(17) ? "" : r[17].ToString();
                    obj.iec_date = r.IsDBNull(18) ? "" : r[18].ToString();
                    obj.iec_verfy = r.IsDBNull(19) ? "" : r[19].ToString();
                    obj.iso = r.IsDBNull(20) ? "" : r[20].ToString();
                    obj.mfg_name = r.IsDBNull(21) ? "" : r[21].ToString();
                    obj.customer = r.IsDBNull(22) ? "" : r[22].ToString();
                }
                catch (Exception ex)
                {
                    LogHelper.writeErrorLog(m_errLogFileName, ex);
                }
                return obj;
            });

           
            mi = miList == null? null: miList.FirstOrDefault();

            if (mi != null)
            {
                mi.b_writenDataBefore = b_writenDataBefore;
            }

#endif

#if SQLSERVER_TengHui
            dic.Add(sp_parm_serial_no, serial);

            conn = connect_string;

            IEnumerable<ModuleInfo> miList = DBOperation.GetMSRowsBySP<ModuleInfo>(conn, "Get_RFID_BasicInfo_V2", dic, r =>
            {
                ModuleInfo obj = new ModuleInfo();
                try
                {
                    obj.ProductType = r.IsDBNull(0) ? "" : r[0].ToString();
                    obj.ELGrade = r.IsDBNull(1) ? "" : r[1].ToString();
                    obj.Status = r.IsDBNull(2) ? "" : r[2].ToString();
                    obj.PackedDate = r.IsDBNull(3) ? "" : r[3].ToString();
                    obj.Pmax = r.IsDBNull(4) ? "" : r[4].ToString();
                    obj.Voc = r.IsDBNull(5) ? "" : r[5].ToString();
                    obj.Isc = r.IsDBNull(6) ? "" : r[6].ToString();
                    obj.Vpm = r.IsDBNull(7) ? "" : r[7].ToString();
                    obj.Ipm = r.IsDBNull(8) ? "" : r[8].ToString();
                    obj.FF = r.IsDBNull(9) ? "" : r[9].ToString();
                    obj.Pivf = r.IsDBNull(10) ? "" : r[10].ToString();
                    obj.Module_ID = r.IsDBNull(11) ? "" : r[11].ToString();
                    obj.PalletNO = r.IsDBNull(12) ? "" : r[12].ToString();
                    obj.CellDate = r.IsDBNull(13) ? "" : r[13].ToString();
                    obj.Cellsource = r.IsDBNull(14) ? "" : r[14].ToString();
                    obj.EqpID = r.IsDBNull(15) ? "" : r[15].ToString();
                    obj.IVFilePath = r.IsDBNull(16) ? "" : r[16].ToString();
                    obj.cell_supplier_country = r.IsDBNull(17) ? "" : r[17].ToString();
                    obj.iec_date = r.IsDBNull(18) ? "" : r[18].ToString();
                    obj.iec_verfy = r.IsDBNull(19) ? "" : r[19].ToString();
                    obj.iso = r.IsDBNull(20) ? "" : r[20].ToString();
                    obj.mfg_name = r.IsDBNull(21) ? "" : r[21].ToString();
                    //obj.customer = r.IsDBNull(22) ? "" : r[22].ToString();
                }
                catch (Exception ex)
                {
                    LogHelper.writeErrorLog(m_errLogFileName, ex);
                }
                return obj;
            });

           
            mi = miList == null? null: miList.FirstOrDefault();

            if (mi != null)
            {
                mi.b_writenDataBefore = b_writenDataBefore;
            }

#endif

#if MYSQL_LOCALHOST
            dic.Add(sp_parm_serial_no, serial);

            conn = connect_string;

            IEnumerable<ModuleInfo> miList = DBOperation.GetMySqlRowsBySP<ModuleInfo>(conn, "Get_RFID_BasicInfo_V2", dic, r =>
            {
                ModuleInfo obj = new ModuleInfo();
                try
                {
                    obj.ProductType = r.IsDBNull(0) ? "" : r[0].ToString();
                    obj.ELGrade = r.IsDBNull(1) ? "" : r[1].ToString();
                    obj.Status = r.IsDBNull(2) ? "" : r[2].ToString();
                    obj.PackedDate = r.IsDBNull(3) ? "" : r[3].ToString();
                    obj.Pmax = r.IsDBNull(4) ? "" : r[4].ToString();
                    obj.Voc = r.IsDBNull(5) ? "" : r[5].ToString();
                    obj.Isc = r.IsDBNull(6) ? "" : r[6].ToString();
                    obj.Vpm = r.IsDBNull(7) ? "" : r[7].ToString();
                    obj.Ipm = r.IsDBNull(8) ? "" : r[8].ToString();
                    obj.FF = r.IsDBNull(9) ? "" : r[9].ToString();
                    obj.Pivf = r.IsDBNull(10) ? "" : r[10].ToString();
                    obj.Module_ID = r.IsDBNull(11) ? "" : r[11].ToString();
                    obj.PalletNO = r.IsDBNull(12) ? "" : r[12].ToString();
                    obj.CellDate = r.IsDBNull(13) ? "" : r[13].ToString();
                    obj.Cellsource = r.IsDBNull(14) ? "" : r[14].ToString();
                    obj.EqpID = r.IsDBNull(15) ? "" : r[15].ToString();
                    obj.IVFilePath = r.IsDBNull(16) ? "" : r[16].ToString();
                    obj.cell_supplier_country = r.IsDBNull(17) ? "" : r[17].ToString();
                    obj.iec_date = r.IsDBNull(18) ? "" : r[18].ToString();
                    obj.iec_verfy = r.IsDBNull(19) ? "" : r[19].ToString();
                    obj.iso = r.IsDBNull(20) ? "" : r[20].ToString();
                    obj.mfg_name = r.IsDBNull(21) ? "" : r[21].ToString();
                }
                catch (Exception ex)
                {
                    LogHelper.writeErrorLog(m_errLogFileName, ex);
                }
                return obj;
            });

           
            mi = miList == null? null: miList.FirstOrDefault();

            if (mi != null)
            {
                mi.b_writenDataBefore = b_writenDataBefore;
            }

#endif

#if MYSQL_CLOUD
            dic.Add(sp_parm_serial_no, serial);

            conn = connect_string;

            IEnumerable<ModuleInfo> miList = DBOperation.GetMySqlRowsBySP<ModuleInfo>(conn, "Get_RFID_BasicInfo_V3", dic, r =>
            {
                ModuleInfo obj = new ModuleInfo();
                try
                {
                    obj.ProductType = r.IsDBNull(0) ? "" : r[0].ToString();
                    obj.ELGrade = r.IsDBNull(1) ? "" : r[1].ToString();
                    obj.Status = r.IsDBNull(2) ? "" : r[2].ToString();
                    obj.PackedDate = r.IsDBNull(3) ? "" : r[3].ToString();
                    obj.Pmax = r.IsDBNull(4) ? "" : r[4].ToString();
                    obj.Voc = r.IsDBNull(5) ? "" : r[5].ToString();
                    obj.Isc = r.IsDBNull(6) ? "" : r[6].ToString();
                    obj.Vpm = r.IsDBNull(7) ? "" : r[7].ToString();
                    obj.Ipm = r.IsDBNull(8) ? "" : r[8].ToString();
                    obj.FF = r.IsDBNull(9) ? "" : r[9].ToString();
                    obj.Pivf = r.IsDBNull(10) ? "" : r[10].ToString();
                    obj.Module_ID = r.IsDBNull(11) ? "" : r[11].ToString();
                    obj.PalletNO = r.IsDBNull(12) ? "" : r[12].ToString();
                    obj.CellDate = r.IsDBNull(13) ? "" : r[13].ToString();
                    obj.Cellsource = r.IsDBNull(14) ? "" : r[14].ToString();
                    obj.EqpID = r.IsDBNull(15) ? "" : r[15].ToString();
                    obj.IVFilePath = r.IsDBNull(16) ? "" : r[16].ToString();
                    obj.cell_supplier_country = r.IsDBNull(17) ? "" : r[17].ToString();
                    obj.iec_date = r.IsDBNull(18) ? "" : r[18].ToString();
                    obj.iec_verfy = r.IsDBNull(19) ? "" : r[19].ToString();
                    obj.iso = r.IsDBNull(20) ? "" : r[20].ToString();
                    obj.mfg_name = r.IsDBNull(21) ? "" : r[21].ToString();
                    obj.customer = r.IsDBNull(22) ? "" : r[22].ToString();
                }
                catch (Exception ex)
                {
                    LogHelper.writeErrorLog(m_errLogFileName, ex);
                }
                return obj;
            });

           
            mi = miList == null? null: miList.FirstOrDefault();

            if (mi != null)
            {
                mi.b_writenDataBefore = b_writenDataBefore;
            }

#endif

            return mi;
            
        }

        
    }

    enum DB_Type { 
        MySql,
        SqlServer,
        Oracle
    }
}
