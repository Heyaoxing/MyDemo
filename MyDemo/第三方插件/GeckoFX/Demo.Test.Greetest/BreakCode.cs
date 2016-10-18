using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Demo.Common.GeckoFxHelper;
using Gecko.DOM;

namespace Demo.Test.Greetest
{
    /// <summary>
    /// 极验验证码破解
    /// </summary>
    public class BreakCode : BaseOperation
    {
        private static int GetPositionX()
        {
            try
            {
                string yposstring = _geckoFxHelper.GetDomAttrByClassFirst(GeckoEnum.HtmlDom.Div, "gt_slice gt_show", "style");
                int ypos = Getypos(yposstring);

                string bgstring = _geckoFxHelper.GetDomAttrByClassFirst(GeckoEnum.HtmlDom.Div, "gt_cut_bg_slice", "style");
                string fullbgstring = _geckoFxHelper.GetDomAttrByClassFirst(GeckoEnum.HtmlDom.Div, "gt_cut_fullbg_slice", "style");

              
                var bg = LoadImage(GetUrlByStyle(bgstring));
                var full = LoadImage(GetUrlByStyle(fullbgstring));
                bg = AlignImage(bg, ypos);
                full = AlignImage(full, ypos);
                return  GetPositionX(bg, full);
            }
            catch (Exception)
            {
            }
            return 0;
        }

        /// <summary>
        /// 获取ypos坐标
        /// </summary>
        /// <param name="yposstring"></param>
        /// <returns></returns>
        private static int Getypos(string yposstring)
        {
            int ypos = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(yposstring))
                    return ypos;
                var split = yposstring.Split(new string[] { "top" }, StringSplitOptions.RemoveEmptyEntries);
                string num = split[1].Replace("'", "");
                num = num.Replace(";", "").Replace(":", "").Replace("px", "");
                if (!string.IsNullOrWhiteSpace(num))
                    ypos = int.Parse(num);
            }
            catch (Exception)
            {
            }
            return ypos;
        }


        /// <summary>
        /// 
        /// </summary>
        private static void AddScript(int positionX)
        {
            //生成的鼠标轨迹数据
            string LoadTrailArray = @"  function LoadTrailArray(nowX, nowY, deltaX) {
                                                        var trailArray = [];
                                                        var cachArray = [];
                                                        var normal = [[1, 0], [2, 0], [1, -1]];//合理距离波动范围
                                                        var abnormal = [4, 6];//异常距离波动范围,用于开头

                                                        var startTime = [5, 8];// 开始拖动阶段时间波动范围,约占5%
                                                        var middleTime = [1, 2];//中间拖动阶段时间波动范围,约占85%
                                                        var endTime = [20, 40];//结束拖动阶段时间波动范围,约占10%
                                                        var overTime = [50, 70];//最后一像素
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
                                                                    else if (i>(count-3)&&i!== count) {
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
            string BreakCode = @"function BreakCode_Insert_A(selector, deltaX) {
                                                var createEvent = function (eventName, ofsx, ofsy) {
                                                    var evt = document.createEvent('MouseEvents');
                                                    evt.initMouseEvent(eventName, true, false, null, 0, 0, 0, ofsx, ofsy, false, false, false, false, 0, null);
                                                    return evt;
                                                };
                                                var delta = deltaX; //要移动的距离,减掉7是为了防止过拟合导致验证失败
                                                delta = delta > 200 ? 200 : delta;
                                                //查找要移动的对象
                                                var obj = document.querySelector(selector);
                                                var a = parseInt(Math.random() * 2) + 21;
                                                var startX = obj.getBoundingClientRect().left + a;
                                                var startY = obj.getBoundingClientRect().top + 18;
                                                var nowX = startX;
                                                var nowY = startY;
                                                var trailArray = LoadTrailArray(startX, startY, delta);
                                                var moveToTarget = function (loopRec) {
                                                    setTimeout(function () {
                                                        nowX = trailArray[loopRec][0];
                                                        nowY = trailArray[loopRec][1];
                                                        obj.dispatchEvent(createEvent('mousemove', nowX, nowY));
                                                        if (trailArray.length <= loopRec + 1) {
                                                            var b = parseInt(Math.random() * 1);
                                                            obj.dispatchEvent(createEvent('mousemove', startX + delta, nowY));
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

            string begin = "BreakCode_Insert_A('.gt_slider_knob'," + positionX + ");";
            if (!_geckoWebBrowser.Document.Body.TextContent.Contains("BreakCode_Insert_A"))
            {
                _geckoFxHelper.RegString(LoadTrailArray, "text/javascript", "script", "body");
                _geckoFxHelper.RegString(BreakCode, "text/javascript", "script", "body");
            }
            _geckoFxHelper.RegString(begin, "text/javascript", "script", "body");

        }

        /// <summary>
        /// 
        /// </summary>
        public static bool RunBreakCode()
        {
           var result= _geckoFxHelper.ExistsElementByClass("gt_ajax_tip success");
            if (!result)
            {
                AddScript(GetPositionX());
            }
            return result;
        }


        /// <summary>
        /// 获取网址链接
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        private static string GetUrlByStyle(string style)
        {
            string url = string.Empty;
            try
            {
                const string pattern = @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                url = r.Match(style).Value;
            }
            catch (Exception ex)
            {
            }
            return url;
        }

        private static Image LoadImage(string url)
        {
            Image image;
            HttpWebRequest hreq = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse hres = (HttpWebResponse)hreq.GetResponse();
            using (var stream = hres.GetResponseStream())
            {
                image = Image.FromStream(stream);
            }
            hres.Close();
            return image;
        }

        private static Image AlignImage(Image img, int ypos = 0, int height = 52)
        {
            const int width = 260;
            Bitmap bmp = new Bitmap(width, height);
            var pos = new int[] {157, 145, 265, 277,181, 169, 241, 253, 109, 97, 289, 301, 85, 73, 25, 37, 13, 1, 121, 133, 61, 49, 217, 229, 205, 193,
                145, 157, 277, 265, 169, 181, 253, 241, 97, 109, 301, 289, 73, 85, 37, 25, 1, 13, 133, 121, 49, 61, 229, 217, 193, 205};
            int dx = 0, sy = 58, dy = 0;
            var g = Graphics.FromImage(bmp);
            for (var i = 0; i < pos.Length; i++)
            {
                g.DrawImage(img, new Rectangle(dx, dy - ypos, 10, 58), new Rectangle(pos[i], sy, 10, 58), GraphicsUnit.Pixel);
                dx += 10;
                if (dx == width)
                {
                    dx = 0;
                    dy = 58;
                    sy = 0;
                }
            }
            g.Dispose();
            return bmp;
        }

        private static int GetPositionX(Image imgBg, Image imgFullBg, Image imgSlice = null)
        {
            var bg = new Bitmap(imgBg);
            var full = new Bitmap(imgFullBg);
            Rectangle rect = new Rectangle(0, 0, bg.Width, bg.Height);
            const int bytesCount = 4;
            var bgData = bg.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            var fullData = full.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int xpos = -1;
            unsafe
            {
                byte* pBg = (byte*)bgData.Scan0;
                byte* pFull = (byte*)fullData.Scan0;
                //sub 2 images
                for (var i = 0; i < bgData.Stride * bgData.Height; i += 4)
                {
                    pBg[i] = (byte)Math.Abs((int)pBg[i] - pFull[i]);
                    pBg[i + 1] = (byte)Math.Abs((int)pBg[i + 1] - pFull[i + 1]);
                    pBg[i + 2] = (byte)Math.Abs((int)pBg[i + 2] - pFull[i + 2]);
                }
                var w = bgData.Width;
                // Roberts edge detect and calculate histgram
                int[] histgram = new int[w];
                int[] histSum = new int[w];
                for (var y = 0; y < bgData.Height - 1; y++)
                {
                    for (var x = 0; x < w - 1; x++)
                    {
                        var i00 = (x + y * w);
                        var i11 = (i00 + w + 1) * bytesCount;
                        var i01 = (i00 + 1) * bytesCount;
                        var i10 = (i00 + w) * bytesCount;
                        i00 *= bytesCount;
                        pFull[i00] = (byte)(Math.Abs(pBg[i00] - pBg[i11]) + Math.Abs(pBg[i01] - pBg[i10])); // b
                        pFull[i00 + 1] = (byte)(Math.Abs(pBg[i00 + 1] - pBg[i11 + 1]) + Math.Abs(pBg[i01 + 1] - pBg[i10 + 1])); // g
                        pFull[i00 + 2] = (byte)(Math.Abs(pBg[i00 + 2] - pBg[i11 + 2]) + Math.Abs(pBg[i01 + 2] - pBg[i10 + 2])); // r
                        histgram[x] += pFull[i00] + pFull[i00 + 1] + pFull[i00 + 2];
                    }
                }
                // find xpos
                int ww = 48, maxValue = -1;
                for (var i = 0; i < ww; i++)
                    histSum[0] += histgram[i];
                for (var x = 1; x < w - ww; x++)
                {
                    histSum[x] = histSum[x - 1] + histgram[x + ww - 1] - histgram[x - 1];
                    if (histSum[x] > maxValue)
                    {
                        xpos = x;
                        maxValue = histSum[x];
                    }
                }
            } // exit unsafe
            bg.UnlockBits(bgData);
            full.UnlockBits(fullData);
            //offset 6 pixels
            return xpos - 6;
        }
    }
}
