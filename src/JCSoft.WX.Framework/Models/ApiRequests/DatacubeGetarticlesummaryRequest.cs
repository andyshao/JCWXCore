﻿using Newtonsoft.Json;
using System;
using System.Globalization;
using JCSoft.WX.Framework.Models.ApiResponses;
using JCSoft.Core.Net.Http;

namespace JCSoft.WX.Framework.Models.ApiRequests
{
    /// <summary>
    /// 获取图文群发每日数据（getarticlesummary）	
    /// </summary>
    public class DatacubeGetarticlesummaryRequest : ApiRequest<DatacubeGetArticlesResponse>
    {
        /// <summary>
        /// 获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        [JsonProperty("begin_date")]
        public string BeginDate { get; set; }

        /// <summary>
        /// 获取数据的结束日期，end_date允许设置的最大值为昨日
        /// </summary>
        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        internal override HttpRequestActionType Method
        {
            get { return HttpRequestActionType.Content; }
        }

        protected override string UrlFormat
        {
            get { return "https://api.weixin.qq.com/datacube/getarticlesummary?access_token={0}"; }
        }

        internal override string GetUrl()
        {
            return String.Format(UrlFormat, AccessToken);
        }

        protected override bool NeedToken
        {
            get { return true; }
        }

        internal override string GetPostContent()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override void Validate()
        {
            //Log(this.GetUrl());
            base.Validate();
            var bdate = DateTime.ParseExact(BeginDate, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            var edate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", CultureInfo.CurrentCulture);

            if (edate >= DateTime.Today)
            {
                throw new ArgumentOutOfRangeException("end_date允许设置的最大值为昨日");
            }
        }
    }
}
