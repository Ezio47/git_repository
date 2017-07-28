using DataBaseClassLibrary;
using ManagerSystemSearchWhereModel.LogicModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.BaseDT.SDE
{
    /// <summary>
    /// 村驻地
    /// </summary>
    public class TD_CUNZHUDI
    {

        /// <summary>
        /// 获取联表查询
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static DataTable getUnionDT(QueryLayerDataSW sw)
        {
            #region union all
            //            select NAME, DISPLAY_X,DISPLAY_Y, FLAG from
            //(  
            //  ( select NAME,DISPLAY_X,DISPLAY_Y, 0 as FLAG from XIANGZHENZHUDI a 
            // where a.DISPLAY_X  is not null and  a.DISPLAY_Y  is not null) UNION  all 
            // ( select NAME,DISPLAY_X,DISPLAY_Y, 1  as FLAG from CUNZHUDI b
            //    where b.DISPLAY_X  is not null and  b.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 2  as FLAG from JIAYOUZHAN c
            //    where c.DISPLAY_X  is not null and  c.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 3  as FLAG from ZERENLUXIAN d
            //    where d.DISPLAY_X  is not null and  d.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 4  as FLAG from ZERENQU e
            //    where e.DISPLAY_X  is not null and  e.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 5  as FLAG from XIAOFANGDUIWU f
            //    where f.DISPLAY_X  is not null and  f.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPALY_X as DISPLAY_X,DISPLAY_Y, 6  as FLAG from ZHENGFUJIGUAN g
            //    where g.DISPALY_X  is not null and  g.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 7  as FLAG from ZIYUAN h
            //    where h.DISPLAY_X  is not null and  h.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 8  as FLAG from CANGKU I
            //    where I.DISPLAY_X  is not null and  I.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 9  as FLAG from ZHUANGBEI J
            //    where J.DISPLAY_X  is not null and  J.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 10  as FLAG from YINGFANG K
            //    where K.DISPLAY_X  is not null and  K.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 11  as FLAG from TINGCHECHANG L
            //    where L.DISPLAY_X  is not null and  L.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 12  as FLAG from LIAOWANGTAI M
            //    where M.DISPLAY_X  is not null and  M.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 13  as FLAG from XUANCHUANBEIPAI N
            //    where N.DISPLAY_X  is not null and  N.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 14 as FLAG from ZHONGJIZHAN O
            //    where O.DISPLAY_X  is not null and  O.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 15 as FLAG from JIANCEZHAN P
            //    where P.DISPLAY_X  is not null and  P.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 16 as FLAG from YINZICAIJIZHAN Q
            //    where Q.DISPLAY_X  is not null and  Q.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 17 as FLAG from GELIDAI R
            //    where R.DISPLAY_X  is not null and  R.DISPLAY_Y  is not null)UNION  all
            //    ( select NAME,DISPLAY_X,DISPLAY_Y, 18 as FLAG from FANGHUOTONGDAO S
            //    where S.DISPLAY_X  is not null and  S.DISPLAY_Y  is not null)
            //     ) as c
            #endregion

            StringBuilder sb = new StringBuilder();
            sb.Append(" select ID, NAME, DISPLAY_X,DISPLAY_Y, FLAG,category,LNGLATSTRS,DBTYPE,TYPE,ImageUrl from ( ");
            sb.Append(" ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 112  as Flag,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE ,'jiayouzhan.png' as ImageUrl from NEW_JIAYOUZHAN b  where b.DISPLAY_X  is not null and  b.DISPLAY_Y  is not null) UNION  all ");
            sb.Append(" ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 113  as Flag,null as category,null as LNGLATSTRS ,null as DBTYPE,0 as TYPE,'location.png' as ImageUrl from CUNZHUDI C where C.DISPLAY_X  is not null and  C.DISPLAY_Y  is not null) UNION  all ");
            sb.Append(" ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 114  as Flag,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE,'location.png' as ImageUrl from POINTMARK D where D.DISPLAY_X  is not null and  D.DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 115  as Flag ,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE,'location.png' as ImageUrl from MOUNTAIN E where E.DISPLAY_X  is not null and  E.DISPLAY_Y  is not null) UNION  all ");
            //sb.Append("   ( select NAME,DISPLAY_X,DISPLAY_Y, 116  as Flag,null as LNGLATSTRS from XIANZHUDI F where F.DISPLAY_X  is not null and  F.DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 117  as Flag,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE,'location.png' as ImageUrl  from NEW_XIANGZHENZHUDI G where G.DISPLAY_X  is not null and  G.DISPLAY_Y  is not null) UNION  all ");
            sb.Append(" ( select [OBJECTID] as  ID, NAME,DISPALY_X as DISPLAY_X,DISPLAY_Y, 128  as FLAG,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE,'zhengfudanwei.png' as ImageUrl  from ZHENGFUJIGUAN g where g.DISPALY_X  is not null and  g.DISPLAY_Y  is not null) UNION  all ");
            sb.Append(" ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 119  as Flag,category,null as LNGLATSTRS,3 as DBTYPE,0 as TYPE,case category when '1' then 'yingfang/ganggou.png' when '2' then 'yingfang/zhuanhun.png' when '3' then 'yingfang/ganghun.png' else 'location.png' end as ImageUrl from YINGFANG G where G.DISPLAY_X  is not null and  G.DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select  [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 120 as Flag,category,null as LNGLATSTRS,12 as DBTYPE,0 as TYPE,'cangku.png' as ImageUrl from CANGKU   where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select  [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 121 as Flag,category,null as LNGLATSTRS,5 as DBTYPE,0 as TYPE,case category when '1' then 'liaowangtai/ganggou.png' when '2' then 'liaowangtai/zhuanhun.png' when '3' then 'liaowangtai/ganghun.png' else 'location.png' end as ImageUrl from LIAOWANGTAI  where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all  ");
            sb.Append("   ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 122 as Flag,category,null as LNGLATSTRS,6 as DBTYPE,0 as TYPE,case category when '1' then 'xuanchuanbeipai/yongjiu.png' when '2' then 'xuanchuanbeipai/linshi.png' else 'location.png' end as ImageUrl from XUANCHUANBEIPAI  where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 123 as Flag,category,null as LNGLATSTRS,7 as DBTYPE,0 as TYPE,case category when '1' then 'zhongjizhan/duanbo.png' when '2' then 'zhongjizhan/chaoduanbo.png' when '3' then 'zhongjizhan/weibo.png' else 'location.png' end as ImageUrl from ZHONGJIZHAN  where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append(" ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 124 as Flag,category,null as LNGLATSTRS,8 as DBTYPE,0 as TYPE,case category when '1' then 'jiancezhan/youxian.png' when '2' then 'jiancezhan/wuxian.png' else 'location.png' end as ImageUrl from JIANCEZHAN where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all  ");
            sb.Append("   ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 125 as Flag,category,null as LNGLATSTRS,9 as DBTYPE,0 as TYPE,case category when '1' then 'yinzicaijizhan/youxian.png' when '2' then 'yinzicaijizhan/wuxian.png' else 'location.png' end as ImageUrl from YINZICAIJIZHAN where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 126 as Flag,category,null as LNGLATSTRS,1 as DBTYPE,0 as TYPE,case category when '1' then 'xiaofangduiwu/zhuanye.png' when '2' then 'xiaofangduiwu/banzhuanye.png' when '3' then 'xiaofangduiwu/yingji.png' when '4' then 'xiaofangduiwu/qunzhong.png' else 'location.png' end as ImageUrl from XIAOFANGDUIWU where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all  ");
            sb.Append(" ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 127 as Flag,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE,'location.png' as ImageUrl from QITASHESHI where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            //sb.Append("  ( select NAME,DISPALY_X as DISPLAY_X,DISPLAY_Y, 128 as Flag,null as LNGLATSTRS from ZHENGFUJIGUAN where DISPALY_X  is not null and  DISPLAY_Y  is not null) UNION  all");
            sb.Append("  ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 129 as Flag ,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE,'tingchechang.png' as ImageUrl from TINGCHECHANG where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append("   ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 212 as Flag,null as category,[shape]  as LNGLATSTRS,null as DBTYPE,1 as TYPE ,'location.png' as ImageUrl from HELIU where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append(" ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 219 as Flag,category,[shape] as LNGLATSTRS,10 as DBTYPE,1 as TYPE,'location.png'as ImageUrl  from GELIDAI where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append(" ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 220 as Flag,category,[shape] as LNGLATSTRS,11 as DBTYPE,1 as TYPE,'location.png'as ImageUrl   from FANGHUOTONGDAO where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 115  as Flag ,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE,'location.png' as ImageUrl from MOUNTAIN E where E.DISPLAY_X  is not null and  E.DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 115  as Flag ,null as category,null as LNGLATSTRS,null as DBTYPE,0 as TYPE,'location.png' as ImageUrl from MOUNTAIN E where E.DISPLAY_X  is not null and  E.DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 221 as Flag,null as category,[shape] as LNGLATSTRS,null as DBTYPE,1 as TYPE,'location.png' as ImageUrl   from ZERENLUXIAN where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all ");
            sb.Append("  ( select [OBJECTID] as  ID, NAME,DISPLAY_X,DISPLAY_Y, 311 as Flag,null as category,[shape] as LNGLATSTRS,null as DBTYPE,2 as TYPE,'location.png' as ImageUrl from ZERENQU where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all  ");
            sb.Append("  ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 313 as Flag,category,[shape] as LNGLATSTRS,10 as DBTYPE,2 as TYPE,'location.png' as ImageUrl from HUOSHAOMIAN where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all  ");
            sb.Append("   ( select [OBJECTID] as  ID,NAME,DISPLAY_X,DISPLAY_Y, 314 as Flag,category,[shape] as LNGLATSTRS,2 as DBTYPE,2 as TYPE ,'location.png' as ImageUrl from ZIYUAN  where DISPLAY_X  is not null and  DISPLAY_Y  is not null) UNION  all   ");
            sb.Append("   ( select [OBJECTID] as  ID,NAME,JD as DISPLAY_X,WD as DISPLAY_Y, 130 as Flag,null as category, null as LNGLATSTRS,null as DBTYPE,0 as TYPE ,'dianzijiankong.png' as ImageUrl from SHIPINGJIANKONG l where l.JD  is not null and  l.WD  is not null) UNION  all   ");
            sb.Append("   ( select [OBJECTID] as  ID,NAME,JD as DISPLAY_X,WD as DISPLAY_Y, 131 as Flag,null as category, null as LNGLATSTRS,null as DBTYPE,0 as TYPE ,'hongwaixiangji.png' as ImageUrl from HONGWAIXIANGJI X where X.JD  is not null and  X.WD  is not null) UNION  all   ");
            sb.Append("   ( select [OBJECTID] as  ID,NAME, DISPLAY_X, DISPLAY_Y, 132 as Flag,null as category, null as LNGLATSTRS,101 as DBTYPE,0 as TYPE ,'youhaishengwujcd.png' as ImageUrl from YHSWJCD X where X.DISPLAY_X  is not null and  X.DISPLAY_Y  is not null) UNION  all   ");
            sb.Append("  ( select  [OBJECTID] as  ID, ([乡]+' 林班:'+[林班]+',小班:'+[小班]) as NAME,  DISPLAY_X , DISPLAY_Y,  316 as Flag,null as category,[shape] as LNGLATSTRS,100 as DBTYPE,2 as TYPE, 'location.png' as ImageUrl from GONGYILINVIEW  ) ");
            //sb.Append("  UNION  all  ( select   [OBJECTID] as  ID, ([乡]+[林班]+[小班]) as NAME, shape.STCentroid().STX AS DISPLAY_X ,shape.STCentroid().STY AS DISPLAY_Y, 316 as Flag,[shape] as LNGLATSTRS,100 as DBTYPE,3 as TYPE from GONGYILIN   where [横坐标]  is not null and  [纵坐标]  is not null) ");
            sb.Append(" ) as ff Where 1=1  ");
            if (!string.IsNullOrEmpty(sw.Name))
            {
                sb.AppendFormat(" And ff.Name like '%{0}%' ", sw.Name.Trim());
            }
            if (!string.IsNullOrEmpty(sw.FlagStr))
            {
                sb.AppendFormat(" And ff.FLAG in ({0})", ClsSql.SwitchStrToSqlIn(sw.FlagStr));
            }
            if (!string.IsNullOrEmpty(sw.AroundValue))//周边距离
            {
                //AND  (dbo.fnGetDistance(LONGITUDE, LATITUDE, 103.2680404, 23.7104433) < 10
                //[dbo].[GetDistanceByNative]
                // sb.AppendFormat(" AND  (dbo.fnGetDistance(DISPLAY_Y, DISPLAY_X,{0},{1}) <= {2})", sw.WD, sw.JD, sw.AroundValue);
                sb.AppendFormat(" AND  (dbo.GetDistanceByNative(DISPLAY_Y,DISPLAY_X,{0},{1}) <= {2})", sw.WD, sw.JD, sw.AroundValue);
            }
            DataSet ds = SDEDataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
    }
}
