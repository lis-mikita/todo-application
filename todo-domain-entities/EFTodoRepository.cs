using System;
using System.Collections.Generic;
using System.Text;
using todo_domain_entities.EF;
using todo_domain_entities.Entities;
using todo_domain_entities.Interfaces;
using todo_domain_entities.Repositories;

namespace todo_domain_entities
{
    public class EFTodoRepository : ITodoRepository
    {
        private readonly TodoDBContext _context;
        private TodoListRepository todoListRepository;
        private TodoItemRepository todoItemRepository;

        public EFTodoRepository(TodoDBContext context)
        {
            _context = context;
        }

        public IRepository<TodoList> TodoLists
        {
            get
            {
                if (todoListRepository == null)
                {
                    todoListRepository = new TodoListRepository(_context);
                }

                return todoListRepository;
            }
        }

        public IRepository<TodoItem> TodoItems
        {
            get
            {
                if (todoItemRepository == null)
                {
                    todoItemRepository = new TodoItemRepository(_context);
                }

                return todoItemRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
