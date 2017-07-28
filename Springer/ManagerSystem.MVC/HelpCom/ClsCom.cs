using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Web;

namespace ManagerSystem.MVC.HelpCom
{
    public class Compare<T, C> : IEqualityComparer<T>
    {
        private Func<T, C> _getField;
        public Compare(Func<T, C> getfield)
        {
            this._getField = getfield;
        }
        public bool Equals(T x, T y)
        {
            return EqualityComparer<C>.Default.Equals(_getField(x), _getField(y));
        }
        public int GetHashCode(T obj)
        {
            return EqualityComparer<C>.Default.GetHashCode(this._getField(obj));
        }
    }
    public static class ClsCom
    {
        /// <summary>
        /// 自定义Distinct扩展方法
        /// </summary>
        /// <typeparam name="T">要去重的对象类</typeparam>
        /// <typeparam name="C">自定义去重的字段类型</typeparam>
        /// <param name="source">要去重的对象</param>
        /// <param name="getfield">获取自定义去重字段的委托</param>
        /// <returns></returns>
        public static IEnumerable<T> MyDistinct<T, C>(this IEnumerable<T> source, Func<T, C> getfield)
        {
            return source.Distinct(new Compare<T, C>(getfield));
        }
    }


    public static class VoiceCom
    {

        public static void ReadVoice()
        {
            SpeechSynthesizer voice = new SpeechSynthesizer();   
            voice.Rate = -2; //设置语速,[-10,10]
            voice.Volume = 100; //设置音量,[0,100]
            voice.Speak("欢迎进入红河州森林生态保护信息指挥系统大数据实时监控,当前监控时间为"+DateTime.Now.ToString("yyyy-MM-dd"));  //播放指定的字符串
        }
    }

}