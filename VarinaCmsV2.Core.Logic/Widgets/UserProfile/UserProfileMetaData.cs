using System;
using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.Widgets.UserProfile
{
    internal class UserProfileMetaData
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
    }
}