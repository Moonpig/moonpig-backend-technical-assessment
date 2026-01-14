# Add to Basket Feature - Acceptance Criteria

## Overview
This feature adds the ability for users to add products to their basket via a REST API endpoint.
The endpoint should validate requests, fetch product details from an external service, and update the basket.

## Acceptance Criteria

### Happy Path
1. **Add product to basket** 
    - A valid product ID and quantity can be posted to the basket, and the product is added successfully.
    - The updated basket is returned in the response.

2. **Retrieve basket** 
    - A valid basket ID can be retrieved, returning all line items and their product details.
    - An empty basket should return successfully with no items.

3. **Add the same product again**
    - Adding the same product more than once should increase its quantity rather than creating a duplicate line.

### Validation
4. **Missing product ID**
    - If no product ID is provided, return an error.

5. **Invalid quantity**
    - Quantities less than 1 should return an error.

6. **Invalid basket ID**
    - If the basket ID is invalid or does not exist, return an error.

### Product Integration
7. **Invalid Product ID**
    - If the product ID does not exist in the product service, return an error.

8. **Product service unavailable**
    - If the product service cannot be reached, return an error gracefully.