$(document).ready(function () {
    $(document).on('click', '.button-plus', function () {
        var quantityField = $(this).siblings('.quantity-field');
        var currentValue = parseInt(quantityField.val());
        if (!isNaN(currentValue) && currentValue < parseInt(quantityField.attr('max'))) {
            quantityField.val(currentValue + 1);
        }
    });
    $(document).on('click', '.button-minus', function () {
        var quantityField = $(this).siblings('.quantity-field');
        var currentValue = parseInt(quantityField.val());
        if (!isNaN(currentValue) && currentValue > 1) {
            quantityField.val(currentValue - 1);
        }
    });

    function checkAuthentication(callback) {
        $.ajax({
            type: "GET",
            url: "/Auth/IsAuthenticated",
            success: function (isAuthenticated) {
                callback(isAuthenticated);
            },
            error: function () {
                alert('Unable to check authentication status.');
            }
        });
    }
    function addToCart(productId, quantity) {
        $.ajax({
            type: "POST",
            url: "/Cart/AddToCart",
            data: {
                productId: productId,
                quantity: quantity
            },
            success: function (response) {
                alert('Product added to cart successfully!');
            },
            error: function (xhr, status, error) {
                alert('An error occurred while adding the product to cart!');
            }
        });
    }

    // Event handler for Add to Cart button click
    $(document).on('click', '#addToCartButton', function () {
        var productId = $('#productId').val();
        var quantity = $('#quantity').val();
        addToCart(productId, quantity);
    });
});
