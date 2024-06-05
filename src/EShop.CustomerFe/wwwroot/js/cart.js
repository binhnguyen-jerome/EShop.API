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
            if (xhr.status === 401) {
                window.location.href = "/auth/login";
            } else {
                alert('An error occurred while adding the product to cart!');
            }
        }
    });
}
$(document).on('click', '.addToCart', function () {
    var productId = $('#productId').val();
    var quantity = $('#quantity').val();
    addToCart(productId, quantity);
});
$(document).on('click', '.updateCart', function () {
    var productId = $('.productId').val();
    var quantity = $('.quantity').val();
    updateCart(productId, quantity);
});
function updateCart(productId, quantity) {
    $.ajax({
        url: '/Cart/UpdateCart',
        type: 'POST',
        data: {
            productId: productId,
            quantity: quantity
        },
        success: function (response) {
            console.log(response);
        },
        error: function (xhr, status, error) {
            if (xhr.status === 401) {
                window.location.href = "/auth/login";
            } else {
                alert('An error occurred while adding the product to cart!');
            }
        }
    });
}