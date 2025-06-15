import React from 'react';
import {
    Form,
    Input,
    Button,
    DatePicker,
    Select,
    Radio,
    Checkbox,
    InputNumber,
    Row,
    Col,
    Space,
    Typography,
    Divider,
    message
} from 'antd';
import {
    UserOutlined,
    LockOutlined,
    PhoneOutlined,
    MailOutlined,
    HomeOutlined,
    HeartOutlined
} from '@ant-design/icons';

const { Title } = Typography;
const { TextArea } = Input;
const { Option } = Select;

const RegisterForm = () => {
    const [form] = Form.useForm();

    const onFinish = (values) => {
        console.log('Form values:', values);
        message.success('Đăng ký thành công!');
    };

    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
        message.error('Vui lòng kiểm tra lại thông tin!');
    };

    const bloodTypes = ['A', 'B', 'AB', 'O'];
    const provinces = ['Hà Nội', 'TP. Hồ Chí Minh', 'Hải Phòng', 'Đà Nẵng', 'Cần Thơ', 'An Giang', 'Bà Rịa - Vũng Tàu', 'Bắc Giang', 'Bắc Kạn', 'Bạc Liêu', 'Bắc Ninh', 'Bến Tre', 'Bình Định', 'Bình Dương', 'Bình Phước', 'Bình Thuận', 'Cà Mau', 'Cao Bằng', 'Đắk Lắk', 'Đắk Nông', 'Điện Biên', 'Đồng Nai', 'Đồng Tháp', 'Gia Lai', 'Hà Giang', 'Hà Nam', 'Hà Tĩnh', 'Hải Dương', 'Hậu Giang', 'Hòa Bình', 'Hưng Yên', 'Khánh Hòa', 'Kiên Giang', 'Kon Tum', 'Lai Châu', 'Lâm Đồng', 'Lạng Sơn', 'Lào Cai', 'Long An', 'Nam Định', 'Nghệ An', 'Ninh Bình', 'Ninh Thuận', 'Phú Thọ', 'Phú Yên', 'Quảng Bình', 'Quảng Nam', 'Quảng Ngãi', 'Quảng Ninh', 'Quảng Trị', 'Sóc Trăng', 'Sơn La', 'Tây Ninh', 'Thái Bình', 'Thái Nguyên', 'Thanh Hóa', 'Thừa Thiên Huế', 'Tiền Giang', 'Trà Vinh', 'Tuyên Quang', 'Vĩnh Long', 'Vĩnh Phúc', 'Yên Bái'];
    const chronicDiseaseOptions = ['Tiểu đường', 'Cao huyết áp', 'Bệnh tim mạch', 'Hen suyễn', 'Bệnh thận', 'Bệnh gan', 'Ung thư', 'HIV/AIDS', 'Viêm gan B/C', 'Khác'];

    return (
        // GIỮ NGUYÊN KHUNG FORM NỀN TRẮNG, BO GÓC, ĐỔ BÓNG
        <div className="w-full max-w-4xl bg-white shadow-2xl rounded-2xl p-8 md:p-12">
            <div className="text-center bg-gradient-to-r from-red-500 to-pink-600 text-white rounded-t-2xl -mt-8 md:-mt-12 -mx-8 md:-mx-12 mb-8 px-8 py-5">
                <HeartOutlined className="text-6xl mb-3" />
                <Title level={2} className="!text-white tracking-tight text-5xl">Đăng Ký tài khoản</Title>
            </div>

            <Form
                form={form}
                name="bloodDonationRegistration"
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                layout="vertical"
                scrollToFirstError
            >
                {/* THAY ĐỔI: XÓA HẾT CÁC CLASS MÀU SẮC ĐỂ TRỞ VỀ MẶC ĐỊNH (ĐEN/XÁM ĐẬM) */}
                <div className="mb-6">
                    <Title level={4} className="mb-4 border-b border-gray-200 pb-2">Thông tin tài khoản</Title>
                    <Row gutter={16}>
                        <Col xs={24} md={8}><Form.Item name="username" label="Tên đăng nhập" rules={[{ required: true, message: 'Nhập tên đăng nhập!' }, { min: 3, message: 'Tối thiểu 3 ký tự!' }]}><Input prefix={<UserOutlined />} placeholder="Tên đăng nhập" /></Form.Item></Col>
                        <Col xs={24} md={8}><Form.Item name="password" label="Mật khẩu" rules={[{ required: true, message: 'Nhập mật khẩu!' }, { min: 6, message: 'Tối thiểu 6 ký tự!' }]}><Input.Password prefix={<LockOutlined />} placeholder="Mật khẩu" /></Form.Item></Col>
                        <Col xs={24} md={8}><Form.Item name="confirmPassword" label="Xác nhận mật khẩu" dependencies={['password']} rules={[{ required: true, message: 'Xác nhận mật khẩu!' }, ({ getFieldValue }) => ({ validator: (_, v) => !v || getFieldValue('password') === v ? Promise.resolve() : Promise.reject('Mật khẩu không khớp!') })]}><Input.Password prefix={<LockOutlined />} placeholder="Xác nhận mật khẩu" /></Form.Item></Col>
                    </Row>
                </div>

                <div className="mb-6">
                    <Title level={4} className="mb-4 border-b border-gray-200 pb-2">Thông tin cá nhân</Title>
                    <Row gutter={16}>
                        <Col xs={24} md={12}><Form.Item name="fullName" label="Họ và tên" rules={[{ required: true, message: 'Nhập họ tên!' }]}><Input placeholder="Họ và tên" /></Form.Item></Col>
                        <Col xs={24} md={12}><Form.Item name="birthDate" label="Ngày sinh" rules={[{ required: true, message: 'Chọn ngày sinh!' }]}><DatePicker className="w-full" format="DD/MM/YYYY" placeholder="Chọn ngày" /></Form.Item></Col>
                    </Row>
                    <Row gutter={16}>
                        <Col xs={24} md={8}><Form.Item name="gender" label="Giới tính" rules={[{ required: true, message: 'Chọn giới tính!' }]}><Radio.Group><Radio value="male">Nam</Radio><Radio value="female">Nữ</Radio><Radio value="other">Khác</Radio></Radio.Group></Form.Item></Col>
                        <Col xs={24} md={8}><Form.Item name="idNumber" label="Số CMND/CCCD" rules={[{ required: true, message: 'Nhập số CMND/CCCD!' }, { pattern: /^[0-9]{9,12}$/, message: 'Không hợp lệ!' }]}><Input placeholder="CMND hoặc CCCD" /></Form.Item></Col>
                        <Col xs={24} md={8}><Form.Item name="bloodType" label="Nhóm máu" rules={[{ required: true, message: 'Chọn nhóm máu!' }]}><Select placeholder="Chọn nhóm máu">{bloodTypes.map(t => <Option key={t} value={t}>{t}</Option>)}</Select></Form.Item></Col>
                    </Row>
                </div>

                <div className="mb-6">
                    <Title level={4} className="mb-4 border-b border-gray-200 pb-2">Thông tin liên hệ</Title>
                    <Row gutter={16}>
                        <Col xs={24} md={8}><Form.Item name="phoneNumber" label="Số điện thoại" rules={[{ required: true, message: 'Nhập số điện thoại!' }, { pattern: /^[0-9]{10,11}$/, message: 'Không hợp lệ!' }]}><Input prefix={<PhoneOutlined />} placeholder="SĐT" /></Form.Item></Col>
                        <Col xs={24} md={8}><Form.Item name="email" label="Email" rules={[{ type: 'email', message: 'Email không hợp lệ!' }]}><Input prefix={<MailOutlined />} placeholder="Email (không bắt buộc)" /></Form.Item></Col>
                        <Col xs={24} md={8}><Form.Item name="province" label="Tỉnh/Thành phố" rules={[{ required: true, message: 'Chọn tỉnh!' }]}><Select placeholder="Chọn tỉnh" showSearch>{provinces.map(p => <Option key={p} value={p}>{p}</Option>)}</Select></Form.Item></Col>
                    </Row>
                    <Row><Col span={24}><Form.Item name="address" label="Địa chỉ hiện tại" rules={[{ required: true, message: 'Nhập địa chỉ!' }]}><Input prefix={<HomeOutlined />} placeholder="Số nhà, đường, phường/xã, quận/huyện" /></Form.Item></Col></Row>
                </div>

                <div className="mb-6">
                    <Title level={4} className="mb-4 border-b border-gray-200 pb-2">Thông tin y tế</Title>
                    <Row gutter={16}>
                        <Col xs={24} md={12}><Form.Item name="hasDonatedBefore" label="Đã từng hiến máu chưa?" rules={[{ required: true, message: 'Vui lòng chọn!' }]}><Radio.Group><Radio value="yes">Có</Radio><Radio value="no">Không</Radio></Radio.Group></Form.Item></Col>
                        <Col xs={24} md={12}>
                            <Form.Item name="donationCount" label="Số lần hiến máu (nếu có)">
                                <InputNumber min={0} placeholder="Số lần" style={{ width: '100%' }} />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Form.Item name="chronicDiseases" label="Bệnh mãn tính (nếu có)"><Checkbox.Group><Row>{chronicDiseaseOptions.map(i => <Col xs={24} sm={12} md={8} key={i}><Checkbox value={i}>{i}</Checkbox></Col>)}</Row></Checkbox.Group></Form.Item>
                    <Form.Item name="diseaseDescription" label="Mô tả chi tiết bệnh (nếu có)"><TextArea rows={3} placeholder="Mô tả chi tiết tình trạng sức khỏe..." /></Form.Item>
                </div>

                <div className="text-center mt-8">
                    <Space size="large">
                        <Button size="large" onClick={() => form.resetFields()} className="!h-12 !px-8">Làm mới</Button>
                        <Button type="primary" htmlType="submit" size="large" className="!h-12 !bg-gradient-to-r !from-red-500 !to-pink-600 !text-white !font-bold hover:!opacity-90 !px-10 !border-none" icon={<HeartOutlined />}>Đăng Ký Hiến Máu</Button>
                    </Space>
                </div>
            </Form>
        </div>
    );
};

export default RegisterForm;