using Dapper;
using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Repositories
{
	public class RoomTypeRepository
	{
		public List<RoomType> GetRoomTypesByBranchId(int branchId)
		{
			using(var conn = new SqlDb().GetConnection("AppDbContext"))
			{
				string sql = @"SELECT * FROM RoomTypes AS RT
							INNER JOIN Branches AS B ON B.Id = RT.BranchId
							WHERE BranchId = @BranchId 
							ORDER BY RT.DisplayOrder";
				
				var result = conn.Query<RoomType, Branch, RoomType>(sql, (roomType, branch) =>
				{
					roomType.Branch = branch;
					return roomType;
				}, new { BranchId = branchId }, splitOn: "Id").ToList();

				return result;
			}			
		}
	}
}