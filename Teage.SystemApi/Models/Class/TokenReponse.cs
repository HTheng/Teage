using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
namespace Teage.SystemApi
{
    public class TokenReponse
    {
        private const string KEYNAME = "USERID_";

        public TokenReponse()
        {
            Interval = 3600 * 24; //默认值
        }

        private ObjectCache _objectCache;
        private ObjectCache ObjectCache
        {
            get
            {
                if (_objectCache == null)
                {
                    _objectCache = MemoryCache.Default;
                }
                return _objectCache;
            }
        }

        public int Interval { get; set; }

        private void Add(string key, object obj)
        {
            DateTimeOffset myTime = DateTimeOffset.Now.AddSeconds(Interval);
            ObjectCache.Add(key, obj, myTime);
        }

        public void Delete(string key)
        {
            ObjectCache.Remove(key);
        }

        public void Update(string key, object obj)
        {
            ObjectCache.Remove(key);
            ObjectCache.Add(key, obj, DateTimeOffset.Now.AddSeconds(Interval));
        }

        public Token First(string key)
        {
            if (string.IsNullOrEmpty(key) || !ObjectCache.Contains(key))
            {
                return null;
            }

            object obj = ObjectCache.Get(key);
            Token myToken = new Token();
            myToken.Key = key;
            myToken.UserId = obj.ToString();

            ObjectCache.Set(key, obj, DateTimeOffset.Now.AddSeconds(Interval));

            return myToken;
        }

        public void Add(Token token)
        {
            ObjectCache.Add(token.Key, token.UserId, DateTimeOffset.Now.AddSeconds(Interval));

            string userIdKey = KEYNAME + token.UserId;
            if (ObjectCache.Contains(userIdKey))
            {
                string oldTokenKey = ObjectCache.Get(userIdKey).ToString();
                if (!oldTokenKey.Equals(token.Key))
                {
                    ObjectCache.Remove(userIdKey);
                    ObjectCache.Add(userIdKey, token.Key, DateTimeOffset.Now.AddSeconds(Interval));
                }

                if (!string.IsNullOrEmpty(oldTokenKey))
                {
                    ObjectCache.Remove(oldTokenKey);
                }
            }
            else
            {
                ObjectCache.Add(userIdKey, token.Key, DateTimeOffset.Now.AddSeconds(Interval));
            }
        }

        public void Update(Token token)
        {
            Update(token.Key, token.UserId);
        }
    }
}