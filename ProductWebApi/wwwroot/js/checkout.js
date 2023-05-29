async function TotalPrice() {
    const priceGet = await fetch(`/api/Shop/totalPrice`,
        {
            method: 'GET',
            mode: 'cors'
        });

    return priceGet.json();
}
async function Price() {
    document.getElementById("totalPrice").innerHTML = "$" + await TotalPrice();
}
Price();


 function CreateList(CartItem) {
     const cartItemHtml = ` <tr id="cartItem${CartItem.cartId}" >
                                <td data-th="Product">
                                    <div class="row">
                                        <div class="col-md-3 text-left">
                                            <img src="${CartItem.product.picture}" alt="" class="img-fluid d-none d-md-block rounded mb-2 shadow ">
                                        </div>
                                        <div class="col-md-9 text-left mt-sm-2">
                                            <h4>${CartItem.product.productName}</h4>
                                            <p class="font-weight-light">${CartItem.product.description}</p>
                                        </div>
                                    </div>
                                </td>
                                <td data-th="Price">$${CartItem.product.price}</td>
                                
                                <td class="actions" data-th="">
                                    <div class="text-right">
                                      
                                        <button onclick="deleteFromCart(${CartItem.cartId})" class="btn btn-white border-secondary bg-white btn-md mb-2">
                                            <i class="fas fa-trash"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>`
    return cartItemHtml;

}

async function deleteFromCart(i) {

    const addItem = await fetch(`/api/Shop/removeCartItem?id=${i}`,
        {
            method: 'DELETE',
            mode: 'cors'
        });
    const item = document.getElementById(`cartItem${i}`);
    item.style.display = "none";
    await Price();
}

async function GetAllUserCart() {
    const cart = await fetch(`/api/Shop/userCart`, {
        method: 'GET',
        mode:'cors'

    });
    return cart.json();
}
async function displayCartList() {
    const cartProduct = await GetAllUserCart();
    document.getElementById("cartBody").innerHTML = null;
    cartProduct.forEach(cart => {
        const cartList =  CreateList(cart);
        document.getElementById("cartBody").innerHTML += cartList;
    });
}
displayCartList();

async function AddToOrder() {
    await fetch(`api/Shop/addToOrder`, {
        method: 'POST',
        mode: 'cors'
    });
    await displayCartList();
    await Price();
}