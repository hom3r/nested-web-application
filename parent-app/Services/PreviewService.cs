using System.Linq;
using Microsoft.EntityFrameworkCore;
using parent.Models;
using parent.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;

namespace parent.Services
{
    public class PreviewService
    {
        private readonly ApplicationDbContext context;

        public PreviewService(ApplicationDbContext _context)
        {
            context = _context;
        }


        public async Task<UserPage> GetUserPage(int userPageId)
        {
            return await context.UserPage.FirstOrDefaultAsync(m => m.ID == userPageId);
        }

        public async Task<UserPage> GetUserPageByHash(string hash)
        {
            UserPagePreview preview = context.UserPagePreview.FirstOrDefault(x => x.Hash == hash);

            if (preview != null)
            {
                int pageId = preview.PageID;
                return await GetUserPage(pageId);
            } else
                return null;
        }

        public List<UserPagePreview> GetPreviews(int pageId)
        {
            return context.UserPagePreview
                                .Where(x => x.Page.ID.Equals(pageId))
                                .ToList();
        }

        public void AddPreview(int pageId)
        {
            var page = context.UserPage.FirstOrDefault(x => x.ID == pageId);

            var preview = new UserPagePreview()
            {
                Invalidated = false,
                Expiration = System.DateTime.Today,
                Page = page,
                Hash = GenerateToken(),
            };

            context.UserPagePreview.Add(preview);
            context.SaveChanges();
        }

        public UserPage UpdateUserPage(int pageId, string name, string content)
        {
            UserPage page = context.UserPage.FirstOrDefault(x => x.ID == pageId);

            if (page != null)
            {
                page.Name = name;
                page.Content = content;
                context.SaveChanges();
            }

            return page;
        }

        public void DeletePreview(int previewId)
        {
            var preview = context.UserPagePreview.Find(previewId);

            if (preview != null)
            {
                context.UserPagePreview.Remove(preview);
                context.SaveChanges();
            }
        }

        private static string GenerateToken(int numberOfBytes = 32)
        {
            return WebEncoders.Base64UrlEncode(GenerateRandomBytes(numberOfBytes));
        }

        private static byte[] GenerateRandomBytes(int numberOfBytes)
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                byte[] byteArray = new byte[numberOfBytes];
                provider.GetBytes(byteArray);
                return byteArray;
            }
        }
    }
}
