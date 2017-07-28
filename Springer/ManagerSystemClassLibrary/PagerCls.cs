using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicClassLibrary;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 分页
    /// </summary>
   public class PagerCls
   {
       #region 分页信息
       /// <summary>
       /// 分页信息 
       /// </summary>
       /// <param name="sw">参见PagerSW</param>
       /// <returns>分页显示Html</returns>
       public static string getPagerInfo(PagerSW sw)
       {
           StringBuilder sb = new StringBuilder();


           int pageCount = sw.rowCount / sw.pageSize;
           if (sw.rowCount % sw.pageSize != 0)
               pageCount = pageCount + 1;

           sw.curPage = getCurPage(sw);
           if (sw.hidePageSize != true)
           {
               sb.AppendFormat("<div class=\"col-sm-1\">");
               sb.AppendFormat("    <div class=\"dataTables_info\" id=\"sample-table-2_info\" >");
               //style=\"width:90px;\" 
               sb.AppendFormat("        <select class=\"form-control\" id=\"selPageSize\"  onchange=\"query()\">");
               string[] arr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "20", "30", "50", "100" };

               if (sw.pageSizeArr != null)
                   arr = sw.pageSizeArr;
               for (int i = 0; i < arr.Length; i++)
               {
                   if (arr[i] == sw.pageSize.ToString())
                       sb.AppendFormat("                <option value=\"{0}\"  selected>{1}</option>", arr[i], arr[i] + "条/页");
                   else
                       sb.AppendFormat("                <option value=\"{0}\" >{1}</option>", arr[i], arr[i] + "条/页");
               }

               sb.AppendFormat("        </select>");
               sb.AppendFormat("    </div>");
               sb.AppendFormat("</div>");
           }
           if (sw.hidePageList != true)
           {
               sb.AppendFormat("<div class=\"col-sm-1\">");
               sb.AppendFormat("    <div class=\"dataTables_info\" id=\"sample-table-2_info\" >");
               //style=\"width:90px;\"
               sb.AppendFormat("        <select class=\"form-control\" id=\"selPage\"  onchange=\"query()\">");

               for (int i = 1; i <= pageCount; i++)
               {
                   if (i == sw.curPage)
                   {
                       sb.AppendFormat("                <option value=\"{0}\"  selected>{1}</option>", i.ToString(), "第" + i.ToString() + "页");
                   }
                   else
                   {
                       sb.AppendFormat("                <option value=\"{0}\">{1}</option>", i.ToString(), "第" + i.ToString() + "页");
                   };
               }
               sb.AppendFormat("        </select>");
               sb.AppendFormat("    </div>");
               sb.AppendFormat("</div>");
           }



           if (sw.hidePageList != true)
           {
               int PageB = 1;//显示开始页
               int PageE = 1;//显示结束页
               if (pageCount <= 9)//当总页数<=9时，显示所有页
                   PageE = pageCount;
               else
               {
                   if (sw.curPage <= 5)//如果当前页面<=5，则显示1~9页
                   {
                       PageE = 9;
                   }
                   else
                   {
                       if (pageCount - sw.curPage <= 4)//如果当前页与总页数相差4，则最后一页为总页数，开始页为最后一页－8
                       {
                           PageE = pageCount;
                           PageB = PageE - 8;
                       }
                       else
                       {
                           PageB = sw.curPage - 4;//开始页往前推4
                           PageE = sw.curPage + 4;//结束页往后推4
                       }
                   }
               }

               sb.AppendFormat("<div class=\"col-sm-10\">");
               sb.AppendFormat("<div class=\"dataTables_paginate paging_bootstrap\">");
               sb.AppendFormat("<ul class=\"pagination\">");
               if (sw.curPage == 1)//第一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-fast-backward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"{0}\"><i class=\"icon-fast-backward\"></i></a></li>", sw.url + "&page=1");

               }
               if (sw.curPage <= 1)//第二页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-backward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"{0}\"><i class=\"icon-backward\"></i></a></li>", sw.url + "&page=" + (sw.curPage - 1).ToString());

               }
               for (int i = PageB; i <= PageE; i++)
               {
                   if (i == sw.curPage)
                   {
                       sb.AppendFormat("<li class=\"active disabled\"><a href=\"#\">{0}</a></li>", i.ToString());
                   }
                   else
                   {
                       sb.AppendFormat("<li class=\"\"><a href=\"{1}\">{0}</a></li>", i.ToString(), sw.url + "&page=" + i.ToString());
                   };
               }
               if (sw.curPage >= pageCount)//后一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-forward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"{0}\"><i class=\"icon-forward\"></i></a></li>", sw.url + "&page=" + (sw.curPage + 1).ToString());
               }
               if (sw.curPage >= pageCount)//最后一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-fast-forward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"{0}\"><i class=\"icon-fast-forward\"></i></a></li>", sw.url + "&page=" + (pageCount).ToString());
               }
           }
           if (sw.hidePageInfo != true)
           {
               if (1 == 1)//总页数
               {
                   string rowB = ((sw.curPage - 1) * sw.pageSize + 1).ToString();
                   string rowE = (sw.curPage * sw.pageSize).ToString();
                   if (sw.curPage * sw.pageSize > sw.rowCount)
                       rowE = sw.rowCount.ToString();
                   if (sw.rowCount == 0)
                       sb.AppendFormat("<li class=\"disabled\"><a>没有记录</a></li>");
                   else
                       sb.AppendFormat("<li class=\"disabled\"><a>显示(" + rowB + "-" + rowE + ")/" + sw.rowCount + "条 " + sw.curPage.ToString() + "/" + pageCount + "页</a></li>");
               }
           }
           sb.AppendFormat("</ul>");
           sb.AppendFormat("</div>");
           sb.AppendFormat("</div>"); 
           return sb.ToString();
       }

       #endregion

       #region 分页信息 防火
       /// <summary>
       /// 分页信息 
       /// </summary>
       /// <param name="sw">参见PagerSW</param>
       /// <returns>分页显示Html</returns>
       public static string getPagerInfo_New(PagerSW sw)
       {
           StringBuilder sb = new StringBuilder();


           int pageCount = sw.rowCount / sw.pageSize;
           if (sw.rowCount % sw.pageSize != 0)
               pageCount = pageCount + 1;

           sw.curPage = getCurPage(sw);

           sb.AppendFormat("<div class=\"divPager\">");
           sb.AppendFormat("<ul class=\"pagination\">");
           if (sw.hidePageSize != true)
           {
               sb.AppendFormat("<li class=\"disabled\">");
               sb.AppendFormat("        <select class=\"form-control\" id=\"selPageSize\" style=\"width:100px;\" onchange=\"query()\">");
               // string[] arr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "20", "30", "50", "100" };
               string[] arr = new string[] { "10", "12", "20", "30", "50", "100" };

               if (sw.pageSizeArr != null)
                   arr = sw.pageSizeArr;
               for (int i = 0; i < arr.Length; i++)
               {
                   if (arr[i] == sw.pageSize.ToString())
                       sb.AppendFormat("                <option value=\"{0}\"  selected>{1}</option>", arr[i], arr[i] + "条/页");
                   else
                       sb.AppendFormat("                <option value=\"{0}\" >{1}</option>", arr[i], arr[i] + "条/页");
               }

               sb.AppendFormat("        </select>");
               sb.AppendFormat("</li>");
           }
           if (sw.hidePageSelect != true)
           {
               sb.AppendFormat("<li class=\"disabled\">");
               //style=\"width:90px;\"
               sb.AppendFormat("        <select class=\"form-control\" id=\"selPage\"  onchange=\"query()\">");

               for (int i = 1; i <= pageCount; i++)
               {
                   if (i == sw.curPage)
                   {
                       sb.AppendFormat("                <option value=\"{0}\"  selected>{1}</option>", i.ToString(), "第" + i.ToString() + "页");
                   }
                   else
                   {
                       sb.AppendFormat("                <option value=\"{0}\">{1}</option>", i.ToString(), "第" + i.ToString() + "页");
                   };
               }

               sb.AppendFormat("        </select>");
               sb.AppendFormat("</li>");
           }




           if (sw.hidePageList != true)
           {
               int PageB = 1;//显示开始页
               int PageE = 1;//显示结束页
               if (pageCount <= 9)//当总页数<=9时，显示所有页
                   PageE = pageCount;
               else
               {
                   if (sw.curPage <= 5)//如果当前页面<=5，则显示1~9页
                   {
                       PageE = 9;
                   }
                   else
                   {
                       if (pageCount - sw.curPage <= 4)//如果当前页与总页数相差4，则最后一页为总页数，开始页为最后一页－8
                       {
                           PageE = pageCount;
                           PageB = PageE - 8;
                       }
                       else
                       {
                           PageB = sw.curPage - 4;//开始页往前推4
                           PageE = sw.curPage + 4;//结束页往后推4
                       }
                   }
               }

               if (sw.curPage == 1)//第一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-fast-backward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"{0}\"><i class=\"icon-fast-backward\"></i></a></li>", sw.url + "&page=1");

               }
               if (sw.curPage <= 1)//第二页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-backward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"{0}\"><i class=\"icon-backward\"></i></a></li>", sw.url + "&page=" + (sw.curPage - 1).ToString());

               }
               for (int i = PageB; i <= PageE; i++)
               {
                   if (i == sw.curPage)
                   {
                       sb.AppendFormat("<li class=\"active disabled\"><a href=\"#\">{0}</a></li>", i.ToString());
                   }
                   else
                   {
                       sb.AppendFormat("<li class=\"\"><a href=\"{1}\">{0}</a></li>", i.ToString(), sw.url + "&page=" + i.ToString());
                   };
               }
               if (sw.curPage >= pageCount)//后一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-forward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"{0}\"><i class=\"icon-forward\"></i></a></li>", sw.url + "&page=" + (sw.curPage + 1).ToString());
               }
               if (sw.curPage >= pageCount)//最后一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-fast-forward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"{0}\"><i class=\"icon-fast-forward\"></i></a></li>", sw.url + "&page=" + (pageCount).ToString());
               }
           }
           if (sw.hidePageInfo != true)
           {
               if (1 == 1)//总页数
               {
                   string rowB = ((sw.curPage - 1) * sw.pageSize + 1).ToString();
                   string rowE = (sw.curPage * sw.pageSize).ToString();
                   if (sw.curPage * sw.pageSize > sw.rowCount)
                       rowE = sw.rowCount.ToString();
                   if (sw.rowCount == 0)
                       sb.AppendFormat("<li class=\"disabled\"><span>没有记录</span></li>");
                   else
                       sb.AppendFormat("<li class=\"disabled\"><span>显示(" + rowB + "-" + rowE + ")/" + sw.rowCount + "条 " + sw.curPage.ToString() + "/" + pageCount + "页</span></li>");
               }
           }
           sb.AppendFormat("</ul>");
           sb.AppendFormat("</div>");
           return sb.ToString();
       }
       #endregion

       #region 分页信息 防火 Ajax调用 getPagerInfoAjax(PagerSW sw)
       /// <summary>
       /// 分页信息 
       /// </summary>
       /// <param name="sw">参见PagerSW</param>
       /// <returns>分页显示Html</returns>
       public static string getPagerInfoAjax(PagerSW sw)
       {
           StringBuilder sb = new StringBuilder();
           if (sw.pageSize == 0)
               sw.pageSize = getDefaultPageSize();
           int pageCount = sw.rowCount / sw.pageSize;
           if (sw.rowCount % sw.pageSize != 0)
               pageCount = pageCount + 1;

           sw.curPage = getCurPage(sw);
           sb.AppendFormat("");
           sb.AppendFormat("<ul class=\"pagination\">");
           if (sw.hidePageSize != true)
           {
               sb.AppendFormat("<li class=\"disabled\">");
               sb.AppendFormat("<select class=\"form-control\" id=\"selPageSize\" style=\"width:100px;\" onchange=\"query('1')\">");
               string[] arr = new string[] { "10", "12", "20", "30", "50", "100" };
               if (sw.pageSizeArr != null)
                   arr = sw.pageSizeArr;
               for (int i = 0; i < arr.Length; i++)
               {
                   if (arr[i] == sw.pageSize.ToString())
                       sb.AppendFormat("                <option value=\"{0}\"  selected>{1}</option>", arr[i], arr[i] + "条/页");
                   else
                       sb.AppendFormat("                <option value=\"{0}\" >{1}</option>", arr[i], arr[i] + "条/页");
               }
               sb.AppendFormat("        </select>");
               sb.AppendFormat("</li>");
           }
           if (sw.hidePageSelect != true)
           {
               sb.AppendFormat("<li class=\"disabled\">");
               sb.AppendFormat(" <select class=\"form-control\" id=\"selPage\"  onchange=\"query(this.value)\">");
               for (int i = 1; i <= pageCount; i++)
               {
                   if (i == sw.curPage)
                   {
                       sb.AppendFormat("<option value=\"{0}\"  selected>{1}</option>", i.ToString(), "第" + i.ToString() + "页");
                   }
                   else
                   {
                       sb.AppendFormat("<option value=\"{0}\">{1}</option>", i.ToString(), "第" + i.ToString() + "页");
                   };
               }
               sb.AppendFormat("</select>");
               sb.AppendFormat("</li>");
           }
           if (sw.hidePageList != true)
           {
               int PageB = 1;//显示开始页
               int PageE = 1;//显示结束页
               if (pageCount <= 9)//当总页数<=9时，显示所有页
                   PageE = pageCount;
               else
               {
                   if (sw.curPage <= 5)//如果当前页面<=5，则显示1~9页
                   {
                       PageE = 9;
                   }
                   else
                   {
                       if (pageCount - sw.curPage <= 4)//如果当前页与总页数相差4，则最后一页为总页数，开始页为最后一页－8
                       {
                           PageE = pageCount;
                           PageB = PageE - 8;
                       }
                       else
                       {
                           PageB = sw.curPage - 4;//开始页往前推4
                           PageE = sw.curPage + 4;//结束页往后推4
                       }
                   }
               }

               if (sw.curPage == 1)//第一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-fast-backward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"#\" onclick=\"query('{0}')\"><i class=\"icon-fast-backward\"></i></a></li>", "1");
               }
               if (sw.curPage <= 1)//第二页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-backward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"#\" onclick=\"query('{0}')\"><i class=\"icon-backward\"></i></a></li>", (sw.curPage - 1).ToString());

               }
               for (int i = PageB; i <= PageE; i++)
               {
                   if (i == sw.curPage)
                   {
                       sb.AppendFormat("<li class=\"active disabled\"><a href=\"#\">{0}</a></li>", i.ToString());
                   }
                   else
                   {
                       sb.AppendFormat("<li class=\"\"><a href=\"#\" onclick=\"query('{0}')\">{1}</a></li>", i.ToString(), i.ToString());
                   }
               }
               if (sw.curPage >= pageCount)//后一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-forward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"#\" onclick=\"query('{0}')\"><i class=\"icon-forward\"></i></a></li>", (sw.curPage + 1).ToString());
               }
               if (sw.curPage >= pageCount)//最后一页
               {
                   sb.AppendFormat("<li class=\"disabled\"><a href=\"#\"><i class=\"icon-fast-forward\"></i></a></li>");
               }
               else
               {
                   sb.AppendFormat("<li class=\"\"><a href=\"#\" onclick=\"query('{0}')\"><i class=\"icon-fast-forward\"></i></a></li>", (pageCount).ToString());
               }
           }
           if (sw.hidePageInfo != true)
           {
               if (1 == 1)//总页数
               {
                   string rowB = ((sw.curPage - 1) * sw.pageSize + 1).ToString();
                   string rowE = (sw.curPage * sw.pageSize).ToString();
                   if (sw.curPage * sw.pageSize > sw.rowCount)
                       rowE = sw.rowCount.ToString();
                   if (sw.rowCount == 0)
                       sb.AppendFormat("<li class=\"disabled\"><span>没有记录</span></li>");
                   else
                       sb.AppendFormat("<li class=\"disabled\"><span>显示(" + rowB + "-" + rowE + ")/" + sw.rowCount + "条 " + sw.curPage.ToString() + "/" + pageCount + "页</span></li>");
               }
           }
           sb.AppendFormat("</ul>"); 
           return sb.ToString();
       }
       #endregion

       #region 获取分页信息


       /// <summary>
       /// 
       /// </summary>
       /// <param name="sw">参见PagerSW</param>
       /// <returns>参见PagerModel</returns>
       public static IEnumerable<PagerModel> getPager(PagerSW sw)
       {
           var result = new List<PagerModel>();
           int pageCount = sw.rowCount / sw.pageSize;
           if (sw.rowCount % sw.pageSize != 0)
               pageCount = pageCount + 1;

           sw.curPage = getCurPage(sw);
           int PageB = 1;//显示开始页
           int PageE = 1;//显示结束页
           if (pageCount <= 9)//当总页数<=9时，显示所有页
               PageE = pageCount;
           else
           {
               if (sw.curPage <= 5)//如果当前页面<=5，则显示1~9页
               {
                   PageE = 9;
               }
               else
               {
                   if(pageCount-sw.curPage<=4)//如果当前页与总页数相差4，则最后一页为总页数，开始页为最后一页－8
                   {
                       PageE = pageCount;
                       PageB = PageE - 8;
                   }
                   else
                   {
                       PageB = sw.curPage - 4;//开始页往前推4
                       PageE = sw.curPage + 4;//结束页往后推4
                   }
               }
           }

           if (1 == 1)//第一页
           {
               PagerModel pm = new PagerModel();
               if (sw.curPage == 1)
               {
                   pm.liClass = "disabled";
                   pm.aHref = "#";
               }
               else
               {

                   pm.liClass = "";
                   pm.aHref = sw.url + "&page=1";
               }
               pm.vlaue = "<i class=\"icon-fast-backward\"></i>";

               result.Add(pm);

           }
           if (1 == 1)//第二页
           {

               PagerModel pm = new PagerModel();
               if (sw.curPage <= 1)
               {
                   pm.liClass = "disabled";
                   pm.aHref = "#";
               }
               else
               {

                   pm.liClass = "";
                   pm.aHref = sw.url + "&page=" + (sw.curPage - 1).ToString();
               }
               pm.vlaue = "<i class=\"icon-backward\"></i>";

               result.Add(pm);
           }
           for (int i =PageB; i <=PageE; i++)
           {
               PagerModel pm = new PagerModel();
               if (i == sw.curPage)
               {

                   pm.liClass = "active disabled";
                   pm.aHref = "#";
                   pm.vlaue = i.ToString();
               }
               else
               {
                   pm.liClass = "";
                   pm.aHref = sw.url + "&page=" + i.ToString();
                   pm.vlaue = i.ToString();
               }
               result.Add(pm);
           }
           if (1 == 1)//后一页
           {
               PagerModel pm = new PagerModel();
               if (sw.curPage >= pageCount)
               {
                   pm.liClass = "disabled";
                   pm.aHref = "#";
               }
               else
               {

                   pm.liClass = "";
                   pm.aHref = sw.url + "&page=" + (sw.curPage + 1).ToString();
               }
               pm.vlaue = "<i class=\"icon-forward\"></i>";

               result.Add(pm);

           }
           if (1 == 1)//最后一页
           {
               PagerModel pm = new PagerModel();
               if (sw.curPage >= pageCount)
               {
                   pm.liClass = "disabled";
                   pm.aHref = "#";
               }
               else
               {

                   pm.liClass = "";
                   pm.aHref = sw.url + "&page=" + (pageCount).ToString();
               }
               pm.vlaue = "<i class=\"icon-fast-forward\"></i>";

               result.Add(pm);

           }
           if (1 == 1)//总页数
           {
               PagerModel pm = new PagerModel();

               pm.liClass = "disabled";
               pm.aHref = "#";
               string rowB = ((sw.curPage - 1) * sw.pageSize+1).ToString();
               string rowE = (sw.curPage * sw.pageSize).ToString();
               if (sw.curPage * sw.pageSize > sw.rowCount)
                   rowE = sw.rowCount.ToString();
               pm.vlaue ="显示("+rowB+"-"+rowE+")/"+sw.rowCount +"条 "+ sw.curPage.ToString() + "/" + pageCount + "页";

               result.Add(pm);
           }
           return result;
       }
       #endregion

       #region 系统默认每页记录数
       /// <summary>
       /// 系统默认每页记录数
       /// </summary>
       /// <returns>默认为10</returns>
       public static int getDefaultPageSize()
       {
           return Convert.ToInt16(ConfigCls.getTableDefaultPageSize());
       }

       #endregion

       #region 获取当前页（防止用户输入页数过大）
       /// <summary>
       /// 获取当前页（防止用户输入页数过大）
       /// </summary>
       /// <param name="sw">参见PagerSW</param>
       /// <returns>当前页数</returns>
       public static int getCurPage(PagerSW sw)
       {
           if (sw.pageSize == 0)
               sw.pageSize = 10;
           int pageCount = sw.rowCount / sw.pageSize;
           if (sw.rowCount % sw.pageSize != 0)
               pageCount = pageCount + 1;
           if (sw.curPage > pageCount)
               sw.curPage = pageCount;
           return sw.curPage;
       }

       #endregion

       #region 每页分页数列表
       /// <summary>
       /// 每页分页数列表
       /// </summary>
       /// <param name="pageSize">每页显示数量</param>
       /// <returns>参见PagerSizeModel</returns>
       public static IEnumerable<PagerSizeModel> getpagerSize(string pageSize)
       {
           var result = new List<PagerSizeModel>();
          // string[] arr = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "20", "30", "50", "100" };
           string[] arr = new string[] { "10", "12", "20", "30", "50", "100" };
           for (int i = 0; i < arr.Length; i++)
           {
               PagerSizeModel m = new PagerSizeModel();
               m.text = arr[i] + "条/页";
               m.value = arr[i];
               if (arr[i] == pageSize)
                   m.selected = " selected";
               result.Add(m);
           }
           return result;
       }

       #endregion
    }
}
