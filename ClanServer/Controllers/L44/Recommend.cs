using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

using eAmuseCore.KBinXML;

using ClanServer.Routing;
using ClanServer.Data.L44;

namespace ClanServer.Controllers.L44
{
    [ApiController, Route("L44")]
    public class RecommendController : ControllerBase
    {
        [HttpPost, Route("8"), XrpcCall("recommend.get_recommend")]
        public async Task<ActionResult<EamuseXrpcData>> GetRecommend([FromBody] EamuseXrpcData data)
        {
            XElement recommend = data.Document.Element("call").Element("recommend");
            XElement player = recommend.Element("data").Element("player");
            _ = int.Parse(player.Element("jid").Value);

            ClanMusicInfo mInfo = await ClanMusicInfo.Instance;

            List<int> recommendedMusic = mInfo.GetRandomSongs(10);
            Random rng = new Random();

            XElement musicList = new XElement("music_list");

            for (int i = 0; i < recommendedMusic.Count; ++i)
            {
                musicList.Add(new XElement("music", new XAttribute("order", i),
                    new KS32("music_id", recommendedMusic[i]),
                    new KS8("seq", (sbyte)rng.Next(3))
                ));
            }

            data.Document = new XDocument(new XElement("response", new XElement("recommend",
                new XElement("data", new XElement("player",
                    musicList
                ))
            )));

            return data;
        }
    }
}
