CREATE OR ALTER PROCEDURE [dbo].[CreateOrder] 	
	@CustomerId int,
	@ProductId int,
	@Quantity int,
	@Id INT OUTPUT,
	@TotalCost float OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

	SELECT @TotalCost = @Quantity*p.Price
	FROM Products p
	WHERE p.ID = @ProductId

	BEGIN TRAN

		INSERT INTO [dbo].[Orders]
			   ([CustomerId]
			   ,[ProductId]
			   ,[Quantity]
			   ,[TotalCost])
		 VALUES
			   (@CustomerId
			   ,@ProductId
			   ,@Quantity
			   ,@TotalCost)
	COMMIT TRAN

	SET @Id = SCOPE_IDENTITY()
END