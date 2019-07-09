# API Testing Notes: 

# Assets Controller
- Compile and Run the solution using IIS Express, the default loading URL is (GET) `https://localhost:44318/assets/products/1.json`
- Json template is of the following (payload is the main json object, product id 1 is shown by default)
```shell
{
    "payload": {
        "id": 1,
        "name": "product1",
        "price": 10000
    },
    "status": {
        "isSuccess": true,
        "code": 200,
        "message": null,
        "validationErrors": null
    }
}
```
- Initial setup products are in IDs 1, 2, 3
- Change product ID to view different products

# Product Controller
- (POST) `https://localhost:44318/api/products/`
- There is no params, a random ID is generated everytime the url is hit
- Use GET url from Assets Controller above to view the new added products.

# Cart Controller
- Uses a memory cache implementation
- (GET) `https://localhost:44318/api/cart`, shows all products in cart
- (POST) `https://localhost:44318/api/cart/add/1` adds product ID 1 to the cart
- (POST) `https://localhost:44318/api/cart/remove/1` removes first found product ID 1 from the cart, keep hitting URL will keep removing subsequent found ID 1 products.