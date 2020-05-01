using System;
using System.Collections.Generic;
using System.Text;

namespace PRACT_OBS.Classes.Data
{
    public class Queries
    {
		//public const string QRY_ON_AIR = "select h.created_at, c.FolderPath, c.Title, a.Name as Artist from djmdSongHistory as h join djmdContent as c on h.ContentID = c.ID join djmdArtist as a on c.ArtistID = a.ID order by h.created_at desc limit 2";
		public const string QRY_ON_AIR = @"select 
												h.ID,
												h.created_at, 
												c.FolderPath, 
												c.Title, 
												a.Name,
												c.ImagePath
											from djmdSongHistory as h
											join djmdContent as c on h.ContentID = c.ID
											join djmdArtist as a on c.ArtistID = a.ID
											order by h.created_at desc
											limit 2";
	}
}
