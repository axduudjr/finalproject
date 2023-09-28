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
		public RoomType GetRoomTypeById(int roomTypeId)
		{
			using (var conn = new SqlDb().GetConnection("AppDbContext"))
			{
				string sql = @"SELECT * FROM RoomTypes AS RT
							INNER JOIN Branches AS B ON B.Id = RT.BranchId
							WHERE RT.Id = @RoomTypeId";

				var result = conn.Query<RoomType, Branch, RoomType>(sql, (roomType, branch) =>
				{
					roomType.Branch = branch;
					return roomType;
				}, new { RoomTypeId = roomTypeId }, splitOn: "Id").FirstOrDefault();

				return result;
			}
		}

		public List<RoomType> GetOtherRoomTypesInBranch(int branchId, int selectedRoomTypeId)
		{
			using (var conn = new SqlDb().GetConnection("AppDbContext"))
			{
				string sql = @"SELECT * FROM RoomTypes AS RT
                    INNER JOIN Branches AS B ON B.Id = RT.BranchId
                    WHERE RT.BranchId = @BranchId AND RT.Id != @SelectedRoomTypeId";

				var result = conn.Query<RoomType, Branch, RoomType>(sql, (roomType, branch) =>
				{
					roomType.Branch = branch;
					return roomType;
				}, new { BranchId = branchId, SelectedRoomTypeId = selectedRoomTypeId }, splitOn: "Id").ToList();

				return result;
			}
		}
		public Room GetRoomTypeBestPrice(int roomTypeId)
		{
			using (var conn = new SqlDb().GetConnection("AppDbContext"))
			{
				string sql = @"SELECT TOP 1 * FROM Rooms AS R
							INNER JOIN RoomTypes AS RT ON RT.Id = R.RoomTypeId
							WHERE RT.Id = @RoomTypeId
							ORDER BY R.Price";

				var result = conn.Query<Room, RoomType, Room>(sql, (room, roomType) =>
				{
					room.RoomType = roomType;
					return room;
				}, new { RoomTypeId = roomTypeId }, splitOn: "Id").FirstOrDefault();

				return result;
			}
		}

	}
}