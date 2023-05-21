




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

 function createProduct(product) {

     const ProductBootstarpCard = `<div class="card m-3" style="width: 18rem;">
  <img class="card-img-top" src="${product.picture}" alt="${product.name}">
  <div class="card-body">
    <h5 class="card-title">${product.productName}</h5>
    <h6 class="card-title">${product.price}</h6>
    <p class="card-text">${product.description}</p>
    <a href="#" class="btn btn-primary">Go somewhere</a>
  </div>
</div>`


     return ProductBootstarpCard;
}

async function displayProducts(i) {
    const products = await getProducts(i);
    document.getElementById("product").innerHTML = null;
    products.forEach(product => {
        const productCard = createProduct(product);
        document.getElementById("product").innerHTML += productCard;
    });
}

displayProducts(1);
function myPageTest(i) {
    displayProducts(i);
}
async function PaginationTest() {
    const size = Object.keys(await getAllProducts()).length;
    var doc = document.getElementById("paginationForCard");
    doc.innerHTML +=`<li class="page-item"><a class="page-link">Previous</a></li>`
    for (var i = 1; i <= (size/5)+1; i++) {

        doc.innerHTML += `<li class="page-item "><a class="page-link" onclick="myPageTest(${i})">${i}</a></li>`;
        
    }
    doc.innerHTML += `<li class="page-item"><a class="page-link">next</a></li>`
}
PaginationTest();

