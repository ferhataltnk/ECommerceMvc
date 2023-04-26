namespace DataAccess.Dapper.Constants

{
    public class Query
    {

        //PRODUCT QUERIES
        public const string QUERY_PRODUCTS_GET_ALL_PRODUCTS = @"
        SELECT * 
        FROM PRODUCTS
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


    }
}
