using System;
using UnityEngine;

namespace WebAPI
{
    [Serializable]
    public class UserData
    {
#if !UNITY_EDITOR
        public string user = "null";
        [TextArea]
        public string accessToken= "null";
        [TextArea]
        public string refreshToken = "null";
#endif
#if UNITY_EDITOR
        [SerializeField] [Tooltip("identificacion del usuario autenticado")]
        public string user = "28bd4da5-7814-43cf-8fec-8e11f41d6f2c";
        [TextArea]
        [SerializeField]
        [Tooltip("accessToken para la autenticacion del usuario")]
        public string accessToken = "eyJraWQiOiJtc2pNc2NYdk0rWjA4UXZVa3VTRFZiWGRFYmZBd0lRSWVqeEZtOGE1NEdnPSIsImFsZyI6IlJTMjU2In0.eyJzdWIiOiIyOGJkNGRhNS03ODE0LTQzY2YtOGZlYy04ZTExZjQxZDZmMmMiLCJpc3MiOiJodHRwczpcL1wvY29nbml0by1pZHAuZXUtd2VzdC0xLmFtYXpvbmF3cy5jb21cL2V1LXdlc3QtMV9GWm1aRnpKR0wiLCJjbGllbnRfaWQiOiIzaDNkN25qZ3F1aHJqNWhqaTB0b25rZG5rayIsIm9yaWdpbl9qdGkiOiJkMTEwMWQ2YS01MDhmLTQyMTYtYmMzZi0xNzBjMDM2OGUzMWUiLCJldmVudF9pZCI6IjZkMDU2Y2ZjLTRhY2ItNDgwNy04NWVjLTczNDFlNzQwY2JkOCIsInRva2VuX3VzZSI6ImFjY2VzcyIsInNjb3BlIjoiYXdzLmNvZ25pdG8uc2lnbmluLnVzZXIuYWRtaW4iLCJhdXRoX3RpbWUiOjE2NjkxNTg2MjUsImV4cCI6MTY2OTE2MjIyNSwiaWF0IjoxNjY5MTU4NjI1LCJqdGkiOiI5OGYxNjRjOC0xY2IzLTQyYjQtODY1OS0wOWYyNGEzMzMxOTEiLCJ1c2VybmFtZSI6IjI4YmQ0ZGE1LTc4MTQtNDNjZi04ZmVjLThlMTFmNDFkNmYyYyJ9.vxuUGPH3irtlJtGj0IgWIguIOZJd0jbOcZeB9_ENA9H28Vo0TJ810UbNeaon11dghf5tpJM93HjbJZiho9FvAp2-7rHXPrVkohkKwGZIjWztcsa5ISm6Zo-Jj2XK8X9a8EbuytE2nZchVk0L27hlS7uMdkcojlSncmpCS6U91czVpUwUl6WXzPY-g_ZuqezznudEin-VrT_n281XA4VgRXIfpxsyEzgZPdV-MBwJNlhhsX1zRpxy9Y3DwzBPrur-CWaSjwBf4gXlx8XHRwPHX1YvdE4bJTWkxAyV_AoygQHUrQxXq7cQ_AFon6xlAg86BylTIgMihz2fptVev5NO7g";
        [TextArea]
        [SerializeField]
        [Tooltip("refreshToken para la autenticacion del usuario")]
        public string refreshToken = "eyJjdHkiOiJKV1QiLCJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAifQ.AnD5SxnBsNEXv59fIVxUwoSlAw_tjmEFYf-NmgYiQmi6DiB5pwSlfIIL4pNDxO-ITE1cselF17FL0GM-IDwPdMf5235CKSGZW1-Ck9PdemG_q2wxwqgeEkQ9aPtxIYbp1ZKPbg4JyoiJKrkLTNdS0nUJ2YyEvzYzeWl2JQh3CwuL--mTx_0wReZLRlMBQ_-_GoNHNiSQCD5RdyhxBTEy14e3s9rQl44wjzrZfScTP70EDK4x5QXeuQ6Vz9J3A9-rvYR2zXM8dVx8Qrl7TWG41ZvZ8yO9xez4G44eRP9fRusai_-m_t_C5wmAjL5fE5JtGNDt9HIdPKuzz2M9Kj9gKw.7J3yv4ReeliFHUKW.7o4QZsxqlTKy9oQnM7VA0ZGUpQSJWPanJIvjEhT4UXHazOXxN7g_2Qw-FIpwde1zjGlWoiCgnt47hiKLkzEMgxr4pkyaTPcBu544V06C19BTQO2iFmFcGYPIScunTWUwZsFj3rKoFmMizAnS7ktGd2M6ZFblpkZj12TsK955mDxvcUQmzLkC4tZG9Ryywproyh0NnBA19Yg3goRFB6PYrFYsxVbptKBZPIvW2Dnhf12tZsv7tlJZNHbC6EfZA3iiCcKghG0gJaxKB_mIb4qCl-1IfR6N92ipNLjTDnXmJr-cG-CEv2MF-jCGFxT2BrSSRYU_PKs416L5m_2RfQl7wUtlQb77sJmEFV5A4D9wzkU0cCE-3vJ0vtXbJHfFqpmCHDVOObPZONUrbNTwhnNpRzpHnOrXDiZ7L9yNf8-_3xAtpzHPi36huYTno6-kDWkDvK9yTKT2fnr-iYet_WnVS2vGvZvjtSrUyZem6ey4Tax_ui6KN15_zOmKH8vPYdt9vG5O05VYjyIkFqQoHKRhUopH-Qmr3Ux82tAyEpPjF5EHEQcjJLaTEF8OaFkoXHCP8bjgTIVgYPYUi1YnMC4pqWeaeeEy3C3pcg49MBVq8AG-I-tVfBg8rHvFrw97MtkI4tPOgn80PkgzXbOoFVUZIhN8PTlRqV_EQ4nu--UoZnZAg8MHfPmtRTmjxXT2wDZvizsYa8KlTCVIzs57dflcTAjPkAopHh6LDAjIvUwptR83ITszqR3-OSOOW36jsFeo_NsTKbRn0UY8Wmk8ev9l_9fWGUMICHJayXmYwsCtcrbZjcCrvXZ6F-KOj1KCi3CNzBwhfj6ZKTRz6kFyheqKckhH4u8QEj19fxjrX8IE4BTrfiBqO4Ws6Ztd91Qfakw0EY7UeFtbNzRz3RaarIiZOaTZdVYJsas8pHKdA0FWXYbiDhdw1dC0RlnINV9Iwo2t64rVKUtMwQp3laz06wUUrrnoGx1fLqDkaC9rMtuq0FR1upf3XP-DyCPFrDpwPU7M9MAzn6ZG8XaFYAONPUcB21JscL-Z1qmIsOC4RREijiaj3fkSKH-9yiUcdhayEpGXp2zpFXIEuQrYahuxhnMB6hrJhW_d-OudvWugVUaDXzZiVo64Co6XonJ0psJUd_JbYxvXmZmsxfZ6vxRf9c0xNIovHQUhiG_p8EzDy1hUUfjc19kGhmQFH6M7uQKRa6qrFn1fSanAkNWhLtJV5peZVTvk6sqDhASvIpCMAgPoMK9VPU65veys7X3bIJEzzMcJv5cu3NZd6HeHUteXiyVWcsaP5gaUgdt4ypLoJp5EerlF76RLKZYeZ8lyDA.q3CoaTw7cmvkl_Lhzp1qug";
#endif

        public UserData()
        {
#if !UNITY_EDITOR
            user = "null";
            accessToken = "null";
            refreshToken =  "null";
#endif
        } 
    }
}