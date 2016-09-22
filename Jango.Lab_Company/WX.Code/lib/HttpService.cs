using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;
using System.IO;
using Zero.Log.Util;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace WX.Code.lib
{
    /// <summary>
    /// http连接基础类，负责底层的http通信
    /// </summary>
    public class HttpService
    {
        public static bool CheckValidationResult(object _sender, X509Certificate _certificate, X509Chain _chain, SslPolicyErrors _errors)
        {
            return true;//直接确认，否则打不开    
        }

        public static string Post(string _xml, string _url, bool _isUseCert, int _timeout)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string _result = "";//返回结果

            HttpWebRequest _request = null;
            HttpWebResponse _response = null;
            Stream _reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (_url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                _request = (HttpWebRequest)WebRequest.Create(_url);

                _request.Method = "POST";
                _request.Timeout = _timeout * 1000;

                //设置POST的数据类型和长度
                _request.ContentType = "text/xml";
                byte[] _data = System.Text.Encoding.UTF8.GetBytes(_xml);
                _request.ContentLength = _data.Length;


                if (_isUseCert)
                {
                    //是否使用证书
                    string _path = HttpContext.Current.Request.PhysicalApplicationPath + WxPayConfig.SSLCERT_PATH;
                    //string _path =  WxPayConfig.SSLCERT_PATH;
                    Log.Info("HttpService", "_path:" + _path);
                    X509Certificate2 _cert = new X509Certificate2(_path, WxPayConfig.SSLCERT_PASSWORD, X509KeyStorageFlags.MachineKeySet);
                    _request.ClientCertificates.Add(_cert);
                    Log.Debug("WXPayApi", "PostXml used cert");
                }

                //往服务器写入数据
                _reqStream = _request.GetRequestStream();
                _reqStream.Write(_data, 0, _data.Length);
                _reqStream.Close();

                //获取服务端返回
                _response = (HttpWebResponse)_request.GetResponse();

                //获取服务端返回数据
                StreamReader _stream = new StreamReader(_response.GetResponseStream(), Encoding.UTF8);
                _result = _stream.ReadToEnd().Trim();
                _stream.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                Log.Error("HttpService", "Thread - caught ThreadAbortException - resetting.");
                Log.Error("Exception message: {0}", e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                Log.Error("HttpService", e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Log.Error("HttpService", "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    Log.Error("HttpService", "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new WxPayException(e.ToString());
            }
            catch (Exception e)
            {
                Log.Error("HttpService", e.ToString());
                throw new WxPayException(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (_response != null)
                {
                    _response.Close();
                }
                if (_request != null)
                {
                    _request.Abort();
                }
            }
            return _result;
        }

        /// <summary>
        /// 处理http GET请求，返回数据
        /// </summary>
        /// <param name="_url">请求的url地址</param>
        /// <returns>http GET成功后返回的数据，失败抛WebException异常</returns>
        public static string Get(string _url)
        {
            System.GC.Collect();
            string _result = "";

            HttpWebRequest _request = null;
            HttpWebResponse _response = null;

            //请求url以获取数据
            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (_url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                _request = (HttpWebRequest)WebRequest.Create(_url);

                _request.Method = "GET";


                //获取服务器返回
                _response = (HttpWebResponse)_request.GetResponse();

                //获取HTTP返回数据
                StreamReader _stream = new StreamReader(_response.GetResponseStream(), Encoding.UTF8);
                _result = _stream.ReadToEnd().Trim();
                _stream.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                Log.Error("HttpService", "Thread - caught ThreadAbortException - resetting.");
                Log.Error("Exception message: {0}", e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                Log.Error("HttpService", e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Log.Error("HttpService", "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    Log.Error("HttpService", "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new WxPayException(e.ToString());
            }
            catch (Exception e)
            {
                Log.Error("HttpService", e.ToString());
                throw new WxPayException(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (_response != null)
                {
                    _response.Close();
                }
                if (_request != null)
                {
                    _request.Abort();
                }
            }
            return _result;
        }
    }
}
