﻿using asp.net_mvc_event_manager.Dtos;
using asp.net_mvc_event_manager.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace asp.net_mvc_event_manager.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == followingDto.FolloweeId))
                return BadRequest("Following already exists.");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
