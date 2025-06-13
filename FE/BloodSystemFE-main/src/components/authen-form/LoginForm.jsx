import React, { useState } from 'react';
import { Button, Input, Form, Checkbox, message } from "antd";
import { FaUser, FaLock } from "react-icons/fa";
import { Navigate, useNavigate } from 'react-router-dom';

// THAY ĐỔI 1: Đổi màu chữ của Logo sang màu tối
const Logo = () => (
  <div className="text-center mb-8">
    <img src="https://cdn-icons-png.flaticon.com/512/2928/2928921.png" alt="LifeStream Logo" className="h-20 w-20 mx-auto mb-4" />
    <h1 className="text-3xl font-bold text-gray-800 tracking-tight">Welcome to LifeStream</h1>
    <p className="text-gray-600 mt-2">Sign in to continue saving lives</p>
  </div>
);

function LoginForm() {
  const navigate = useNavigate();
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);

  const handleLogin = (values) => {
    console.log("Login values:", values);
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
      if (values.email === "admin@test.com" && values.password === "password") {
        message.success("Login successful! Redirecting...");
      } else {
        message.error("Invalid email or password!");
      }
    }, 2000);
  };

  return (
    // Div này không cần thay đổi, nó chỉ dùng để căn giữa
    <div className="min-h-screen flex items-center justify-center">
      {/* THAY ĐỔI 2: Nền form đổi thành màu trắng và có bóng đổ */}
      <div
        className="bg-white shadow-2xl rounded-2xl p-10 sm:p-14 w-full max-w-md"
      >
        <Logo />
        <Form
          form={form}
          onFinish={handleLogin}
          layout="vertical"
          className="space-y-6"
        >
          <Form.Item
            name="email"
            rules={[
              { required: true, message: "Please input your email!" },
              { type: 'email', message: "Please enter a valid email!" }
            ]}
          >
            <Input
              // THAY ĐỔI 3: Icon màu xám, bỏ hết style nền tối
              prefix={<FaUser className="mr-3 text-gray-400 text-lg" />}
              placeholder="Email"
              size="large"
              className="!h-14 !text-base" // Giữ lại kích thước
            />
          </Form.Item>

          <Form.Item
            name="password"
            rules={[{ required: true, message: "Please input your password!" }]}
          >
            <Input.Password
              // THAY ĐỔI 4: Icon màu xám, bỏ hết style nền tối
              prefix={<FaLock className="mr-3 text-gray-400 text-lg" />}
              placeholder="Password"
              size="large"
              className="!h-14 !text-base" // Giữ lại kích thước
            />
          </Form.Item>

          <Form.Item>
            <div className="flex justify-between items-center">
              <Form.Item name="remember" valuePropName="checked" noStyle>
                {/* THAY ĐỔI 5: Chữ Checkbox màu tối, bỏ style tùy chỉnh */}
                <Checkbox>
                  <span className="text-base text-gray-700">Remember me</span>
                </Checkbox>
              </Form.Item>
              <span
                className="font-medium text-red-600 hover:text-red-500 hover:underline cursor-pointer"
                onClick={() => navigate('/forgot-password')}
              >
                Forgot password?
              </span>

            </div>
          </Form.Item>

          <Form.Item>
            {/* Nút bấm giữ nguyên vì đã rất đẹp và là điểm nhấn */}
            <Button
              type="primary"
              htmlType="submit"
              size="large"
              loading={loading}
              className="w-full !bg-gradient-to-r !from-red-500 !to-pink-600 !text-white !font-bold hover:!opacity-90 transition-all duration-300 transform hover:scale-105 !h-14 !text-lg !border-none"
            >
              Sign In
            </Button>
          </Form.Item>

          <div className="text-center">
            <p className="text-gray-700 text-base">
              Don't have an account?{" "}
              <span
                onClick={() => navigate('/register')}
                className="font-medium text-red-600 hover:text-red-500 hover:underline cursor-pointer"
              >
                Create Account
              </span>
            </p>
          </div>
        </Form>
      </div>
    </div>
  );
}
export default LoginForm;