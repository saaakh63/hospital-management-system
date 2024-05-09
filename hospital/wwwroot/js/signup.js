document.getElementById("registerForm").onsubmit=function register(){
   // alert("1");

    let users=JSON.parse(localStorage.getItem("users"))||{};   // { username: {email:"","phoneNumber":"","password":"","gender":""} }   //users[username][email]

    let user= document.getElementById("user").value;
    if(user in users){
        alert("Username exists");
        return;
    }
    //alert("2");

    let email= document.getElementById("Email").value;
    let emailRe=/\w+@gmail\.com$/i;  //hala@gmail.com
    let validateEmail= emailRe.test(email);
    if(validateEmail === false){
        alert("your email must have @gmail.com");
        return;
    }
    //alert("3");

    emailSearch='"email":"'+email+'"';
    if(JSON.stringify(users).includes(emailSearch)){
        alert("email already exist");
        return;
    }
    //alert("4");

    let phoneNum= document.getElementById("PhoneNumber").value;
    let phoneReg= /^0(10|11|12|15)\d{8}$/; 
    let validateNumber= phoneReg.test(phoneNum);
    if(validateNumber=== false){
        alert("start with 010, 011, 012, or 015");
        return;
    }
    //alert("5");

    let pass=document.getElementById("password").value;
    let confirmpass= document.getElementById("comfirm password").value;
    let passReg= /\w{8,}/;
    //alert(MD5(pass));

    let validatepassword=passReg.test(pass);
    if(validatepassword === false && pass!= confirmpass){
        alert ("Weak password the minimum length is 8 chars");
        return;
    }
    //alert("7");

    let gender= document.getElementById("gender").value;
    if(!(gender==="female" || gender==="male")){
        alert("your gender is male or female");
        return;
    }

    //alert("8");

    let newuser={"email":email, "phoneNumber":phoneNum, "password":MD5(pass), "gender":gender}
    users[user]=newuser;
    localStorage.setItem("users",JSON.stringify(users));
}