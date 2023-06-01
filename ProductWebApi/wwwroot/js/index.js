


async function CountOfItem() {
    const countGet = await fetch(`/api/Shop/countOfItem`,
        {
            method: 'GET',
            mode: 'cors'
        });
   
    return countGet.json();
}
async function CartCounter() {
    document.getElementById("countOfItems").innerHTML = await CountOfItem();
}
CartCounter();
async function getProducts(i) {
   
    if (i == null) {
        i = 1;
    } 
    const response = await fetch(`/api/Product/pagenation?page=${i}`,
        {
            method: 'GET',
            mode:'cors'
        });
    const products = await response.json();
    return products;
}



async function getAllProducts() {

    
    const response = await fetch(`/api/Product/Products`,
        {
            method: 'GET',
            mode: 'cors'
        });
    const products = await response.json();
    return products;
}

function createProduct(currentItem) {

     const card = `<div class="card m-3 shadow-lg p-3 mb-5 bg-white rounded" style="width: 18rem;">
            <img class="card-img-top" src="${currentItem.picture}" alt="${currentItem.productName}">
            <div class="card-body">
                <h5 class="card-title">${currentItem.productName}</h5>
                <h6 class="card-title">${currentItem.price}$</h6>
                <p class="card-text">${currentItem.description}</p>
                <button type="button" onclick="addTocart(${currentItem.productId})" class="btn btn-primary">Add to cart</button>
            </div>
        </div>`;

     return card;
}

async function addTocart(i) {
   
    
    await fetch(`/api/Shop/addToCart?productId=${i}`,
        {
            method: 'POST',
            mode: 'cors'
        });
    await CartCounter();
}

async function displayProducts(i) {
    const products = await getProducts(i);
   
    document.getElementById("product").innerHTML = null;
    products.items.forEach(product => {
        const productCard = createProduct(product);
        document.getElementById("product").innerHTML += productCard;
    });
}

displayProducts(1);
function myPageTest(i) {
    displayProducts(i);
}
async function PaginationTest() {
    const products = await getProducts(i);
   
    var doc = document.getElementById("paginationForCard");
    doc.innerHTML += `<li class="page-item"><a  class="page-link">Previous</a></li>`

    for (var i = 1; i <= products.totalPage; i++) {

        doc.innerHTML += `<li class="page-item "><a class="page-link" onclick="myPageTest(${i})">${i}</a></li>`;
        
    }
    doc.innerHTML += `<li class="page-item"><a  class="page-link">next</a></li>`
    
}
PaginationTest();
async function getFoundProducts() {



    var word = document.getElementById("searchProduct").value;

    const response = await fetch(`/api/Product/SearchProductsAndGetAll?word=${word}`,
        {
            method: 'GET',
            mode: 'cors'
        });
    const products = await response.json();
    return products;
}
async function foundProductsDisplay() {
    const products = await getFoundProducts();
    document.getElementById("product").innerHTML = null;
    products.forEach(product => {
        const productCard = createProduct(product);
        document.getElementById("product").innerHTML += productCard;
    });
}
document.getElementById("eventFromJs").onclick = function () {
    foundProductsDisplay();
}





