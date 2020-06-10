using Backend_Develin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Develin.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ProjectContext _context;
        private readonly DbSet<Comment> _comments;


        public CommentRepository(ProjectContext dbContext)
        {
            _context = dbContext;
            _comments = _context.Comments;
        }
        public void Add(Comment comment)
        {
            _comments.Add(comment);
        }

        public void Delete(Comment comment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetBy(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
