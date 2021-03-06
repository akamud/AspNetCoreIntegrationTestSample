﻿using AspNetCoreIntegrationTestSample.IntegrationTests.Suporte;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIntegrationTestSample.IntegrationTests.Specs
{
    public class BlogRepositorioTestes : TesteBaseBanco
    {
        // Exemplo de teste com Save
        [Test]
        public async Task InserirDeveSalvarBlogComPosts()
        {
            var blog = new Blog
            {
                Url = "http://high5devs.com",
            };
            blog.Posts.Add(new Post
                {
                    Content =
                        "Muitas pessoas chamam o Github de rede social para pessoas desenvolvedoras, e acho até que faz um sentido.",
                    Title = "Um segredo: O repositório especial do Github!"
                }
            );
            
            var repositorio = GetService<BlogsRepositorio>();

            await repositorio.InserirBlog(blog);

            var blogDoBanco = _contextParaTestes.Blogs
                .Include(x => x.Posts)
                .FirstOrDefault();
            blogDoBanco.Should().BeEquivalentTo(blog);
        }

        // Exemplo de teste com Get
        [Test]
        public async Task ObterBlogDeveTrazerBlogPorIdComPosts()
        {
            var blog = new Blog
            {
                Url = "http://high5devs.com",
            };
            blog.Posts.Add(new Post
                {
                    Content =
                        "Muitas pessoas chamam o Github de rede social para pessoas desenvolvedoras, e acho até que faz um sentido.",
                    Title = "Um segredo: O repositório especial do Github!"
                }
            );

            _contextParaTestes.Add(blog);
            _contextParaTestes.SaveChanges();

            var repositorio = GetService<BlogsRepositorio>();

            var blogDoBanco = await repositorio.ObterBlog(blog.BlogId);

            blogDoBanco.Should().BeEquivalentTo(blog);
        }
    }
}