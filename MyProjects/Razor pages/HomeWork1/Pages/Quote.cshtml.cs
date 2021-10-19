using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HomeWork1.Model;

namespace HomeWork1.Pages
{
    public class QuoteModel : PageModel
    {
        public string Quote { get; set; }
        public string Author { get; set; }
        public void OnGet()
        {
            List<DearDiary> quotes = new List<DearDiary> {
                new DearDiary { Quote = "Be yourself, everyone else is already taken.", Author = "Oscar Wilde"},
                new DearDiary { Quote = "What’s done can’t be undone.", Author = "William Shakespeare"},
                new DearDiary { Quote = "I could easily forgive his pride, if he had not mortified mine.", Author = "Jane Austen"},
                new DearDiary { Quote = "God gave us memory so that we might have roses in December.", Author = "James Matthew Barrie"},
                new DearDiary { Quote = "Beauty is in the eye of the gazer.", Author = "Charlotte Bronte"},
                new DearDiary { Quote = "Although I may not be yours, I can never be another’s.", Author = "Mary Shelley"},
                new DearDiary { Quote = "The things that make me different are the things that make me.", Author = "Alan Alexander Milne"},
                new DearDiary { Quote = "To the well-organized mind, death is but the next great adventure.", Author = "J.K.Rowling"},
                new DearDiary { Quote = "If man knew how women pass the time when they are alone, they’d never marry.", Author = "O.Henry"},
                new DearDiary { Quote = "The gods play no favorites.", Author = "Charles Bukowski" }
            };

            Random rand = new Random();
            int index = rand.Next() % (quotes.Count - 1) + 1;
            Quote = quotes.ElementAt(index).Quote;
            Author = quotes.ElementAt(index).Author;
        }
    }
}
