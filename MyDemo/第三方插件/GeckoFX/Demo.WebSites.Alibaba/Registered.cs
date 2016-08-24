using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Common.GeckoFxHelper;
using Gecko.DOM;

namespace Demo.WebSites.Alibaba
{
    public class Registered : BaseOperation
    {
        public static void SlideMouse()
        {
            //生成的鼠标轨迹数据
            string loadTrailArray = @"  function LoadTrailArray(nowX, nowY, deltaX) {
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

            //模拟移动鼠标
            string script = @"function MouseSlide_Insert_A() {
                                                var createEvent = function (eventName, ofsx, ofsy) {
                                                    var evt = document.createEvent('MouseEvents');
                                                    evt.initMouseEvent(eventName, true, false, null, 0, 0, 0, ofsx, ofsy, false, false, false, false, 0, null);
                                                    return evt;
                                                };
                                                var delta = 300+ parseInt(Math.random() *20); //要移动的距离,加10个随机像素防止拟合
                                                //查找要移动的对象
                                                var obj=document.getElementById('nc_1_n1z');
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
                                            };";

            using (var document = _geckoWebBrowser.Document.GetElementById("alibaba-register-box"))
            {
                var iframe = new GeckoIFrameElement(document.DOMElement).ContentDocument;
                if (!iframe.Body.TextContent.Contains("BreakCode_Insert_A"))
                {
                    _geckoFxHelper.IframeRegString("alibaba-register-box", loadTrailArray, "text/javascript", "script", "body");
                    _geckoFxHelper.IframeRegString("alibaba-register-box", script, "text/javascript", "script", "body");
                }
                _geckoFxHelper.IframeRegString("alibaba-register-box", "MouseSlide_Insert_A();", "text/javascript", "script", "body");
            }
        }



        public static void SlideMouses()
        {
            //生成的鼠标轨迹数据
            string loadTrailArray = @"  function LoadTrailArray(nowX, nowY, deltaX) {
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

            //模拟移动鼠标
            string script = @"function MouseSlide_Insert_A() {
                                                var createEvent = function (eventName, ofsx, ofsy) {
                                                    var evt = document.createEvent('MouseEvents');
                                                    evt.initMouseEvent(eventName, true, false, null, 0, 0, 0, ofsx, ofsy, false, false, false, false, 0, null);
                                                    return evt;
                                                };
                                                var delta = 300+ parseInt(Math.random() *20); //要移动的距离,加10个随机像素防止拟合
                                                //查找要移动的对象
                                                var obj=document.getElementById('nc_1_n1z');
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
                                            };";

                if (!_geckoWebBrowser.Document.Body.TextContent.Contains("BreakCode_Insert_A"))
                {
                    _geckoFxHelper.RegString( loadTrailArray, "text/javascript", "script", "body");
                    _geckoFxHelper.RegString( script, "text/javascript", "script", "body");
                }
                _geckoFxHelper.RegString( "MouseSlide_Insert_A();", "text/javascript", "script", "body");
        }


    }
}
