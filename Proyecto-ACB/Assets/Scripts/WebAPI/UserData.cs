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
        public string accessToken = "eyJraWQiOiJtc2pNc2NYdk0rWjA4UXZVa3VTRFZiWGRFYmZBd0lRSWVqeEZtOGE1NEdnPSIsImFsZyI6IlJTMjU2In0.eyJzdWIiOiIyOGJkNGRhNS03ODE0LTQzY2YtOGZlYy04ZTExZjQxZDZmMmMiLCJpc3MiOiJodHRwczpcL1wvY29nbml0by1pZHAuZXUtd2VzdC0xLmFtYXpvbmF3cy5jb21cL2V1LXdlc3QtMV9GWm1aRnpKR0wiLCJjbGllbnRfaWQiOiIzaDNkN25qZ3F1aHJqNWhqaTB0b25rZG5rayIsIm9yaWdpbl9qdGkiOiI1NWU3MTgwZi05OGFjLTRhNjgtYWQ5OS1kMDUxYjI5YmQ0MmEiLCJldmVudF9pZCI6IjM2ZjJjZDBlLWM4ODQtNDkxNC05ZDk3LTc3MjdhZDY5ODI0NSIsInRva2VuX3VzZSI6ImFjY2VzcyIsInNjb3BlIjoiYXdzLmNvZ25pdG8uc2lnbmluLnVzZXIuYWRtaW4iLCJhdXRoX3RpbWUiOjE2NjkxNTkxODcsImV4cCI6MTY2OTE2Mjc4NywiaWF0IjoxNjY5MTU5MTg3LCJqdGkiOiJlNDQ2MGNmYi04MzU1LTQxNjYtODM1Yi1jYzU4NWI4MTFiNmMiLCJ1c2VybmFtZSI6IjI4YmQ0ZGE1LTc4MTQtNDNjZi04ZmVjLThlMTFmNDFkNmYyYyJ9.CgV7hP8sAQuw2NnnbxBOMO0qxykh2qDmpUzr2yW4-Mznz8rsNyPzbeQEtx8S-98AHEnWiHHHeQhqOsXN7L248UK4A0xyKpVL-BM8w3g6J7nfYoZRfB7DLlu7ojDySOyxV9tAQMmSN8p31hY2k_Fwvg2KspI5_BUH0md7BX9URusbqs43tzTgpPWStTIXgXgF4jaCUGVAS0pXbhiAPSB367Ikut-B_UN7tlss5C-TMM-JgpJiPwgY2SvezaB_o4GwPaWp6Sfu9Xni8cZ9uLtL8Xw4ojO-xyMDOm-tdFhIG3j-QN_xZuIKFWLc4EgC9XfZAV9jfpeucH1swp3zYOJuSg";
        [TextArea]
        [SerializeField]
        [Tooltip("refreshToken para la autenticacion del usuario")]
        public string refreshToken = "eyJjdHkiOiJKV1QiLCJlbmMiOiJBMjU2R0NNIiwiYWxnIjoiUlNBLU9BRVAifQ.h5KFIVCLvzSQKYynwQPS5rq1RHR7pXuItthsfmhBM5d9mi51E1Rw5BgpzNCoXn8_Y7X17OShezIUyu6JvFQJsaxs-c9DhtrVwGtIpQTPaJc5q9HWikB-I1oSGfdSaZwbjeZHyOAa2rwOPKhYYp8eRw2AezB-Oq1VF9Q0H-kQyVZQ1P79ILVvdy7ODsrCi44MXnkncfeR76DpsbILKzUu264xLeuvXhzHy7yVWMQuXp6f7D2zl7JJpfcCaHLygQkB3Q4ZDbCxPoATRiIOteNSwvP34dGYLs_3Dfeim3MKvMGvSYXWYCVGBQXmNhgiUfaiyw8xdiEcEb59LXv9AZIJVA.ocPx3avqzRE6uQJA.mXm4PQEj43YgvcVeQBvwjJs99i_Kk-Zu-ajBCPXFNt7a-Ba5rm_xNn5g_1Jzm4QU0lebHiT3PpWnLCikAa70LqhNpf9I5GatgN8OTNIlmEpL0FIKI6HDzy9lZN_fnndKGKmOE_af-Z80C_V308RI88AtcIjTRJmeqps1SgQPnKw31CRVvalfb1y7Dfpc923cxoi6iREnLnf68fHtkC_f5K1QzlW2n6TfaGMoIR-LBQitlzhNnFs6VSO8jse9-9fXUsnIyU3ho-PfRZrhubOCGxnLFHgSh5M_9bKVnaSRZvW4Wn4ovXuSTCOAwdk3W05jfK5kP504TMvhyRf4w59zXlgIzmVgKwy5IYFaCypmAykoJg0rqfQ8b2uwj4Sm1cHQp-hFMEAllK6IAYvF2_mFqhkc6U2KHjsYc8U9h6HRs-104L6p1JCn5pqtYJ5K1cJ3bBKuRp8qEdsEKN2mjpksbPrK78FISnFgoxsJWqfrct9SvbmbQc24UUaY1zqPHPd5XKWiY2awXNXRzIeoJoYOujR9NJWJ8W-zEHiczy-B1oB8ydbUr2kPGpUKJOYW-ALBTmWiTS7IVvB1UeUY-EyfrQ36nNXw2GZ8U4oydylpn9zt0c2eELNU5t87-7EonB2iai5bQLO9cNElIMr4tLGRuQqwWW6BwYNUY-OMIRF2olrMOu9IT0xPk0H-cLdfqrhOZs48Z8hUpyHEmfi9o7NI4iZOTlWRnO2p8rwp74Zk-FHo3lH89Au18YsVta980cWmKiJqiXDLI-xV__-GX1rjTtrJcSM-G1_o748Ux1S2vKkIyFxwcIXRf-DDoSNbMRv-S97yWPR5BcksZu8lSBWVGh2L_2Z-nrTnuFdUv441uLuA1V-0tSHU-h6i3zmUtBh_zGZ4K0y-q73arj-_u0vPuHzmQWyPYOUkI9GiPs4y4z2lDf3qUvCEDCeD2g9XoxA41oC0voM4neMB7XwyIkagv9Zx2UYgKivVEvKPb9Ex2grt5aVYcquiizOxrUzecpX8v1dBmnLq39FIFrKTErc8adqREThbUsBBXHEc8JmRPGuUwBOLPxC3pizjm8yYEmSdrA_qUV93rGi1krhy3MI039ArPYl98bdIHiqLknvGY6rsHc8HH0JX6iVuUGtV3_7K3uqKjo6HUlcsAJQhitSLbqP73VU31Orzvw9sM02wCablLKIDLQWkhLf_uO1ONed24Xa3DUmSHdWOqYD69ZHhtFvdBdwUuMv2FhDBsCG_VtkEKN5EiRrr0DZ5TFMLeOn8uYQJpo34ILr6PqWZTxKiUmtBRC7fliDZjf5DT4YiZzu-gB-M0H31cJuOig.T-Jav6ot0hWjQJo8e7uXDg";
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