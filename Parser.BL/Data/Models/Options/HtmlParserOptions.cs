namespace Parser.BL.Data.Models.Options
{
    public class HtmlParserOptions
    {
        public TagInfo CardList { get; set; }

        public TagInfo CardItem { get; set; }

        public TagInfo Promo { get; set; }

        public TagInfo Brand { get; set; }

        public TagInfo Title { get; set; }

        public TagInfo Feedbacks { get; set; }

        public TagInfo PriceWithoutDiscount { get; set; }

        public TagInfo PriceWithDiscount { get; set; }

        public string DeleteTagName { get; set; }

        public string AdvertClassName { get; set; }

        public string IdAttributeName { get; set; }
    }
}
