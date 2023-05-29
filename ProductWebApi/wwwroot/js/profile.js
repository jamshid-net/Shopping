 async function ProfileInfo() {
    const getuser = await fetch(`/api/Shop/aboutuser`,
        {
            method: 'GET',
            mode: 'cors'
        }); 
    return  getuser.json();
   
}
 
async function SetProfile() {
    const user = await ProfileInfo();
    document.getElementById("username").innerHTML = null;
    document.getElementById("useremail").innerHTML = null;
    document.getElementById("username").innerHTML = user.userName;
    document.getElementById("useremail").innerHTML = user.email;
}
SetProfile(); 


async function GetUserOrder() {
    const ordersOfUser = await fetch(`/api/Shop/getUserOrders`, {
        method: 'GET',
        mode: 'cors'
    });
    const ordersjson = await ordersOfUser.json();
    return ordersjson;
}

async function HtmlModelUserOrder(order) {
    
    const orderHtml = `<tr>
                            <td>${order.orderId}</td>
                            <td>${order.orderDate}</td>
                            <td>$${order.totalPrice}</td>
                            <td>${order.isDelivered ? 'Delivered' : 'Processing'}</td>
                       </tr>`;
    return orderHtml;

}
async function DisplayOrderUser() {
    const orders = await GetUserOrder();
    document.getElementById("orderOfUser").innerHTML = ""; 
    orders.forEach(async (order) => { 
        const orderList = await HtmlModelUserOrder(order); 
        document.getElementById("orderOfUser").innerHTML += orderList;
    });
}

 DisplayOrderUser();