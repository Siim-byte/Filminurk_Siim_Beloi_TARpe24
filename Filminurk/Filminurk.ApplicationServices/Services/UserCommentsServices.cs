using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.ApplicationServices.Services
{
    public class UserCommentsServices : IUserCommentsServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IUserCommentsServices _userCommentsServices;
        public UserCommentsServices
            (
            FilminurkTARpe24Context context,
            IUserCommentsServices userCommentsServices
            )
        {
            _context = context;
            _userCommentsServices = userCommentsServices;

        }
        public async Task<UserComment> NewComment(UserCommentDTO newcommentDTO)
        {
            UserComment domain = new UserComment();
            domain.CommentID = Guid.NewGuid();
            domain.CommentBody = newcommentDTO.CommentBody;
            domain.CommenterUserID = newcommentDTO.CommenterUserID;
            domain.CommentedScore = newcommentDTO.CommentedScore;
            domain.CommentCreatedAt = DateTime.Now;
            domain.CommentModifiedAt = DateTime.Now;
            domain.IsHelpful = newcommentDTO.IsHelpful;
            domain.IsHarmful = newcommentDTO.IsHarmful;
             
            await _context.UserComments.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
