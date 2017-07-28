using ManagerSystemModel.SDEModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.SDECLS
{
    /// <summary>
    /// 公益林 空间数据库
    /// </summary>
    public class GONGYILINCls
    {
        #region 获取列表
        /// <summary>
        /// 获取公益林List
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IEnumerable<SDE_GONGYILIN_Model> getListModel(SDE_GONGYILIN_Model model)
        {
            var result = new List<SDE_GONGYILIN_Model>();
            DataTable dt = BaseDT.SDE.GONGYILIN.getDT(model);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var m = GetModel(dt, i);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取列表分页
        /// <summary>
        /// 获取列表分页
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<SDE_GONGYILIN_Model> getModelPager(SDE_GONGYILIN_Model sw, out int total)
        {
            var result = new List<SDE_GONGYILIN_Model>();
            DataTable dt = BaseDT.SDE.GONGYILIN.getDT(sw, out total);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var m = GetModel(dt, i);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region Private
        /// <summary>
        /// 获取公益林模型
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private static SDE_GONGYILIN_Model GetModel(DataTable dt, int i)
        {
            SDE_GONGYILIN_Model m = new SDE_GONGYILIN_Model();
            m.AGE = dt.Rows[i]["年龄"].ToString();
            m.AGEGROUP = dt.Rows[i]["龄组"].ToString();
            m.AGELEVEL = dt.Rows[i]["龄级"].ToString();
            m.AREA = dt.Rows[i]["所属区"].ToString();
            m.ASIAFOREST = dt.Rows[i]["亚林种"].ToString();
            m.AVGHEIGHT = dt.Rows[i]["平均高"].ToString();
            m.AVGSEA = dt.Rows[i]["平均海"].ToString();
            m.AVGXIONG = dt.Rows[i]["平均胸"].ToString();
            m.BAREROCK = dt.Rows[i]["基岩裸"].ToString();
            m.CANOPY = dt.Rows[i]["郁闭度"].ToString();
            m.CHECKPERSON = dt.Rows[i]["核查人"].ToString();
            m.CHECKTIME = dt.Rows[i]["核查时"].ToString();
            m.COMMUNITYNODE = dt.Rows[i]["群落结"].ToString();
            m.COMPENSATIONFACE = dt.Rows[i]["补偿面"].ToString();
            m.COUNTRY = dt.Rows[i]["乡"].ToString();
            m.COUNTY = dt.Rows[i]["县"].ToString();
            m.DEDUCTFACE = dt.Rows[i]["扣除面"].ToString();
            m.DESERTIFICATION = dt.Rows[i]["荒漠化"].ToString();
            m.DESERTIFICATION_ = dt.Rows[i]["荒漠化_"].ToString();
            m.DOMINANTTREE = dt.Rows[i]["优势树"].ToString();
            m.EARTH = dt.Rows[i]["地貌"].ToString();
            m.EARTHCLASS = dt.Rows[i]["地类"].ToString();
            m.ECOLOGICAL = dt.Rows[i]["生态功"].ToString();
            m.ECOLOGICAL_ = dt.Rows[i]["生态功_"].ToString();
            m.ECOLOGICALAREA = dt.Rows[i]["生态区"].ToString();
            m.FORESTHEALTH = dt.Rows[i]["森林健"].ToString();
            m.FORESTLANDSHI = dt.Rows[i]["林地使"].ToString();
            m.FORESTLANDSUO = dt.Rows[i]["林地所"].ToString();
            m.FORESTSHI = dt.Rows[i]["林木所"].ToString();
            m.FORESTSUO = dt.Rows[i]["林木所"].ToString();
            m.LINBAN = dt.Rows[i]["林班"].ToString();
            m.LOCATIONNAME = dt.Rows[i]["区位名"].ToString();
            m.MANAGEMENTLEVEL = dt.Rows[i]["管理级"].ToString();
            m.MANAGEMENTPERSON = dt.Rows[i]["管护人"].ToString();
            m.MANAGEMENTXING = dt.Rows[i]["管护形"].ToString();
            m.MANAGEMENTZE = dt.Rows[i]["管护责"].ToString();
            m.NATURALNESS = dt.Rows[i]["自然度"].ToString();
            m.OBJECTID = dt.Rows[i]["OBJECTID"].ToString();
            m.OBJECTID_1 = dt.Rows[i]["OBJECTID_1"].ToString();
            m.PERHECTARE = dt.Rows[i]["每公顷"].ToString();
            m.POWER = dt.Rows[i]["事权等"].ToString();
            m.PROJECT = dt.Rows[i]["保护等"].ToString();
            m.PROJECTAREA = dt.Rows[i]["工程区"].ToString();
            m.RESPONSIBILITY = dt.Rows[i]["责任单"].ToString();
            m.RIGHT = dt.Rows[i]["权属者"].ToString();
            m.RIVER = dt.Rows[i]["流域"].ToString();
            m.ROCKY = dt.Rows[i]["石漠化"].ToString();
            m.SANDCLASS = dt.Rows[i]["沙化类"].ToString();
            m.SANDDEGREE = dt.Rows[i]["沙化程"].ToString();
            m.SHAPE_LENG = dt.Rows[i]["Shape_Leng"].ToString();
            m.SLOPEDU = dt.Rows[i]["坡度"].ToString();
            m.SLOPEPOSITION = dt.Rows[i]["坡位"].ToString();
            m.SLOPEXIANG = dt.Rows[i]["坡向"].ToString();
            m.SMALLFACE = dt.Rows[i]["小班面"].ToString();
            m.SMALLVILLAGERS = dt.Rows[i]["村民小"].ToString();
            m.SOILNAME = dt.Rows[i]["土壤名"].ToString();
            m.SOURCE = dt.Rows[i]["起源"].ToString();
            m.STATECITY = dt.Rows[i]["州市代"].ToString();
            m.TOTALVEGETATION = dt.Rows[i]["植被总"].ToString();
            m.TREENODE = dt.Rows[i]["树种结"].ToString();
            m.TREESPECIESGROUP = dt.Rows[i]["树种组"].ToString();
            m.UNIT = dt.Rows[i]["单位性"].ToString();
            m.VILLAGE = dt.Rows[i]["村"].ToString();
            m.WHOLECLASS = dt.Rows[i]["全小班"].ToString();
            m.X = dt.Rows[i]["纵坐标"].ToString();
            m.XIAOBAN = dt.Rows[i]["小班"].ToString();
            m.Y = dt.Rows[i]["横坐标"].ToString();
            m.STX = dt.Rows[i]["STX"].ToString();
            m.STY = dt.Rows[i]["STY"].ToString();
            return m;
        }
        #endregion
    }
}
