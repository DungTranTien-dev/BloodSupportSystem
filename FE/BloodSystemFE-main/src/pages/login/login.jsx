import React from 'react'
import LoginForm from '../../components/authen-form/LoginForm';

function LoginPage() {
  return (
    // Gộp 2 div lại và áp dụng các class cần thiết ở đây
    <div className="h-screen overflow-hidden flex items-center justify-center bg-[url('https://img.lovepik.com//back_pic/05/64/03/535b618c44b9dde.jpg_wh860.jpg')] bg-cover bg-center bg-no-repeat p-4">
      <LoginForm />
    </div>
  )
}

export default LoginPage