using System.IO;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreMVC.Models;
using OnlineStoreMVC.Services;

namespace OnlineStoreMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContex contex;

        public readonly IWebHostEnvironment environment;

		public ProductsController(ApplicationDbContex contex,IWebHostEnvironment environment)
        {
            this.contex = contex;
			this.environment = environment;
		}
        public IActionResult Index()
        {

            var products = contex.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductDto productDto)
		{
            if (productDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The Image is Required");
            }
            if(!ModelState.IsValid)
            {
                return View(productDto);
            }


            // save the image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/products/" + newFileName;

            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDto.ImageFile.CopyTo(stream);
            }

            // save the new product in the database
            Product product = new Product()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Price = productDto.Price,
                Description = productDto.Description,
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now,
            };

            contex.Products.Add(product);
            contex.SaveChanges();
			return RedirectToAction("Index","Products");
		}

        public IActionResult Delete(int id)
        {

			var product = contex.Products.Find(id);

			if (product == null)
			{
				return RedirectToAction("Index", "Products");
			}

			string ImageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
			System.IO.File.Delete( ImageFullPath);
            contex.Products.Remove( product );
            contex.SaveChanges(true);
			return RedirectToAction("Index", "Products"); 

		}
        public IActionResult Edit(int id)
        {
            var product = contex.Products.Find(id);

            if (product == null)
            { return RedirectToAction("Index", "Products");
            }

            // create productDto from product
            var productDto = new ProductDto()
            {
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,
               
        };


            ViewData["ProductId"]	= product.Id;

			ViewData["ImageFileName"] = product.ImageFileName;
			ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy") ;
			return View(productDto);
		}
        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto)
        {
            var product = contex.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index", "Products ");
            }
            if (!ModelState.IsValid)
            {

                ViewData["ProductId"] = product.Id;

                ViewData["ImageFileName"] = product.ImageFileName;
                ViewData["CreatedAt"] = product.CreatedAt.ToString("MM/dd/yyyy");
                return View(productDto);

            }

            // update the image file if we have a new image file
            string newFileName = product.ImageFileName;
            if (productDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productDto.ImageFile.FileName);
                string imageFullPath = environment.WebRootPath + "/products/" + newFileName;

                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDto.ImageFile.CopyTo(stream);

                }
                //delete old img
                string oldImageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath); 
            }

			// update the product in the database
			product.Name = productDto.Name;
			product.Brand = productDto.Brand;
			product.Category = productDto.Category;
			product.Price = productDto.Price;
			product.Description= productDto.Description;
			
            product.ImageFileName= newFileName;
            contex.SaveChanges();
            return RedirectToAction("Index","Products");
		}
	}
}
