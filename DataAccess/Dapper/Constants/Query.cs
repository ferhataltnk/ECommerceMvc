namespace DataAccess.Dapper.Constants

{
    public class Query
    {

        //PRODUCT QUERIES
        public const string QUERY_PRODUCTS_GET_BY_PRODUCTID = @"
        SELECT * 
        FROM PRODUCTS
		WHERE ProductId = @productId
        ";
        public const string QUERY_PRODUCTS_GET_PRODUCTS_BY_CATEGORYID = @"
        SELECT *
        FROM Products 
        WHERE CategoryId=@categoryId
        ";

        public const string QUERY_PRODUCTS_GET_PRODUCTS_BY_SEARCHFILTER = @"
	    SELECT *
        FROM Products P
	    INNER JOIN Brands B
		    ON B.BrandId=P.Brand
	    INNER JOIN Colors C
		    ON C.ColorId=P.Color
        INNER JOIN SubCategories S
		    ON S.SubCategoryId=P.SubCategory
        WHERE Price BETWEEN @MinPrice AND @MaxPrice
            AND CategoryId=@categoryId
            AND B.BrandName IN (
                SELECT Value
                FROM string_split(@brand,','))
            AND C.ColorName IN (  
                SELECT Value
                FROM string_split(@color,','))
            AND S.SubCategoryName IN (
			    SELECT Value
                FROM string_split(@subCategories,','))
        ";




        //REVIEW QUERIES
        public const string QUERY_REVIEWS_GET_REVIEW_BY_PRODUCTID = @"
        SELECT * 
        FROM REVIEWS 
        WHERE ProductId = @productId
        ";

        public const string QUERY_REVIEWS_INSERT_INTO_REVIEW = @"
        INSERT INTO Reviews (
	            ProductId,
	            UserId,
	            Comment,
	            Rating,
	            ReviewDate
	            )
        VALUES (
	            @ProductId,
	            @UserId,
	            @Comment,
	            @Rating,
	            GETDATE()
	            )
        ";


        //SUBCATEGORY QUERIES
        public const string QUERY_SUBCATEGORIES_GET_SUBCATEGORIES_BY_CATEGORYID = @"
        SELECT S.SubCategoryName
	    FROM SubCategories S
	    INNER JOIN SubCategoriesOfCategories Sc
		    ON S.SubCategoryId=Sc.SubCategoryId
	    INNER JOIN Categories C
		    ON C.CategoryId=Sc.CategoryId
	    WHERE C.CategoryId=@categoryId
        ";
    }
















}
