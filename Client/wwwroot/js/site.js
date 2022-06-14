// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// membuat kode javascript sendiri

// menggunakan onclick
$('button').on('click', function () {
    alert("Ini halaman home");
})

// menggunakan addEventListener
document.getElementById("paragraf1").addEventListener("mouseover", myFunction1);
document.getElementById("paragraf1").addEventListener("mouseout", myFunction4);
document.getElementById("paragraf2").addEventListener("mousemove", myFunction2);
document.getElementById("paragraf2").addEventListener("mouseout", myFunction5);
document.getElementById("paragraf3").addEventListener("mousemove", myFunction3);
document.getElementById("paragraf3").addEventListener("mouseout", myFunction6);

function myFunction1() {
    paragraf1.style.color = "blue";
}
function myFunction2() {
    paragraf2.style.color = "blue";
}
function myFunction3() {
    paragraf3.style.color = "blue";
}
function myFunction4() {
    paragraf1.style.color = "black";
}
function myFunction5() {
    paragraf2.style.color = "black";
}
function myFunction6() {
    paragraf3.style.color = "black";
}

console.log("tes latihan")

