using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.Common.GeckoFxHelper;

namespace Demo.WebSites
{
    public class JcNet : BaseOperation
    {
        public const string URL = "http://www.jc.net.cn/jsp/front/regist_detail.jsp?register_type=3";


        /// <summary>
        /// 执行破解程序
        /// 超时时间20秒
        /// 10秒后检测到不成功则再调用一次破解程序
        /// </summary>
        /// <returns></returns>
        public static bool CrackRun()
        {

            //破解结果
            bool result = false;
            //拖动验证码破解
            SliderBreaking();
            //检测频率
            int crackcount = 2000;
            while (crackcount > 0)
            {
                if (IsSuccess())
                {
                    result = true;
                    break;
                }
                //10秒不成功后再调用执行一次破解
                if (crackcount == 1000 )
                {
                    SliderBreaking();
                }

                Thread.Sleep(10);
                Application.DoEvents();
                crackcount--;
            }
            return result;
        }


        /// <summary>
        /// 是否成功
        /// </summary>
        /// <returns></returns>
        private static bool IsSuccess()
        {
            bool result = false;
            using (var document = _geckoWebBrowser.Document.GetElementById("iphone-inside"))
            {
                if (document.GetAttribute("style").Contains("none"))
                {
                    result = true;
                }
            }
            return result;
        }

        private static void SliderBreaking()
        {
            if (!_geckoWebBrowser.Document.Body.TextContent.Contains("JcNet_MouseSlide_Insert_A"))
            {
                _geckoFxHelper.RegString(script, "text/javascript", "script", "body");
            }
            _geckoFxHelper.RegString("JcNet_MouseSlide_Insert_A();", "text/javascript", "script", "body");
        }

        /// <summary>
        /// 拖动脚本
        /// </summary>
        private const string script = @"  //模拟移动鼠标
                                          function JcNet_MouseSlide_Insert_A() {
                                                var createEvent = function (eventName, ofsx, ofsy) {
                                                    var evt = document.createEvent('MouseEvents');
                                                    evt.initMouseEvent(eventName, true, false, null, 0, 0, 0, ofsx, ofsy, false, false, false, false, 0, null);
                                                    return evt;
                                                };
                                                var delta = $('.bgSlider')[0].offsetWidth; 
                                                //查找要移动的对象
                                                var obj=$('.ui-draggable')[0];
                                                var startX = obj.getBoundingClientRect().left +  parseInt(Math.random() * 5) + 20;
                                                var startY = obj.getBoundingClientRect().top +  parseInt(Math.random() * 5)+13;
                                                var nowX = startX;
                                                var nowY = startY;
                                                var trailArray = LoadTrailArray(startX, startY, delta);
                                                var moveToTarget = function (loopRec) {
                                                    setTimeout(function () {
                                                        nowX = trailArray[loopRec][0];
                                                        nowY = trailArray[loopRec][1];
                                                        obj.dispatchEvent(createEvent('mousemove', nowX, nowY));
                                                        if (trailArray.length <= loopRec + 1) {
                                                            obj.dispatchEvent(createEvent('mousemove', startX + delta , nowY));
                                                            obj.dispatchEvent(createEvent('mouseup', startX + delta, nowY));
                                                        } else {
                                                            moveToTarget(loopRec+1);
                                                        }
                                                    }, trailArray[loopRec][2]);
                                                };
                                                obj.dispatchEvent(createEvent('mousedown', startX, startY));
                                                moveToTarget(0);
                                                return trailArray;
                                            };
 
                                       //生成的鼠标轨迹数据
                                       function LoadTrailArray(nowX, nowY, deltaX) {
                                                        var trailArray = [];
                                                        var cachArray = [];
                                                        var normal = [[1, 0], [2, 0], [1, -1]];//合理距离波动范围
                                                        var abnormal = [4, 6];//异常距离波动范围,用于开头

                                                        var startTime = [20, 30];// 开始拖动阶段时间波动范围,约占5%
                                                        var middleTime = [3, 10];//中间拖动阶段时间波动范围,约占85%
                                                        var endTime = [50, 80];//结束拖动阶段时间波动范围,约占10%
                                                        var overTime = [150, 250];//最后一像素
                                                        var cachX = nowX;
                                                        var cachY = nowY;
                                                        var abnor= parseInt(Math.random() * 9) + 1;
                                                        while (true) {

                                                            if (cachArray.length < abnor) {
                                                                cachX = cachX + parseInt(Math.random() * (abnormal[1] - abnormal[0]) + abnormal[0]);
                                                                cachY = cachY + parseInt(Math.random() * (abnormal[1] - abnormal[0]) + abnormal[0]);
                                                                cachArray.push([cachX, cachY]);
                                                                continue;
                                                            }

                                                            var random = parseInt((normal.length - 1) * Math.random());
                                                            cachX = cachX + normal[random][0];
                                                            cachY = cachY + normal[random][1];
                                                            cachArray.push([cachX, cachY]);

                                                            if (cachX > nowX + deltaX) {
                                                                var count = cachArray.length;
                                                                var wavetime = [];
                                                                for (var i = 0; i < count; i++) {
                                                                    if (i < 3)
                                                                        wavetime = startTime;
                                                                    else if (i === count - 1) {
                                                                        wavetime = overTime;
                                                                    }
                                                                    else if (i>(count-4)&&i!== count) {
                                                                        wavetime = endTime;
                                                                    } else {
                                                                        wavetime = middleTime;
                                                                    }
                                                                    var settime = parseInt((wavetime[1] - wavetime[0]) * Math.random()) + wavetime[0];
                                                                    trailArray.push([cachArray[i][0], cachArray[i][1], settime]);
                                                                }
                                                                return trailArray;
                                                            }
                                                        }
                                                    }";
    }
}
