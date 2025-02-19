﻿using PawFect.Business.Abstract;
using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        } 
        public void Create(Comment entity)
        {
            _commentDal.Create(entity);
        }

        public void Delete(Comment entity)
        {
           _commentDal.Delete(entity);
        }

        public Comment GeyById(int id)
        {
            return _commentDal.GetById(id);
        }

        public void Update(Comment entity)
        {
            _commentDal.Update(entity);
        }
    }
}
