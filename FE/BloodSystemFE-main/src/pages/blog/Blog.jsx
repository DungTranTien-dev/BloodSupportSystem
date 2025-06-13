import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { FaArrowRight, FaCalendarAlt, FaUser } from 'react-icons/fa';

// Dữ liệu mẫu cho các bài blog
const blogPosts = [
  // Thêm nhiều bài viết để kiểm tra phân trang
  { id: 1, title: "Tầm quan trọng của việc hiến máu định kỳ", category: "Kiến thức y học", image: "https://images.unsplash.com/photo-1524721696987-b9527df9e512?q=80&w=2940&auto=format&fit=crop", author: "Dr. Anna", date: "15 Tháng 7, 2024", excerpt: "Hiến máu không chỉ cứu người mà còn mang lại nhiều lợi ích sức khỏe cho chính người hiến. Hãy cùng tìm hiểu..." },
  { id: 2, title: "Câu chuyện từ người được cứu sống nhờ máu hiến tặng", category: "Câu chuyện cộng đồng", image: "https://images.unsplash.com/photo-1576091160550-2173dba999ef?q=80&w=2940&auto=format&fit=crop", author: "Minh Anh", date: "12 Tháng 7, 2024", excerpt: "Tôi đã từng đứng giữa ranh giới sự sống và cái chết. Và chính những giọt máu nhân ái đã cứu sống tôi..." },
  { id: 3, title: "Chuẩn bị gì trước khi đi hiến máu?", category: "Hướng dẫn", image: "https://images.unsplash.com/photo-1605152276857-859c4b574244?q=80&w=2848&auto=format&fit=crop", author: "Y tá Hạnh", date: "10 Tháng 7, 2024", excerpt: "Để buổi hiến máu diễn ra thuận lợi, bạn cần chuẩn bị tốt về sức khỏe và tinh thần. Dưới đây là những lưu ý quan trọng." },
  { id: 4, title: "Ngày hội hiến máu 'Chủ Nhật Đỏ' thành công rực rỡ", category: "Sự kiện", image: "https://images.unsplash.com/photo-1556761175-5973dc0f32e7?q=80&w=3132&auto=format&fit=crop", author: "Ban tổ chức", date: "08 Tháng 7, 2024", excerpt: "Sự kiện đã thu hút hàng ngàn người tham gia, thu về hàng nghìn đơn vị máu quý giá cho cộng đồng." },
  { id: 5, title: "Nhóm máu hiếm và tại sao chúng lại quan trọng", category: "Kiến thức y học", image: "https://images.unsplash.com/photo-1581091226825-a6a2a5aee158?q=80&w=2940&auto=format&fit=crop", author: "Dr. Nam", date: "05 Tháng 7, 2024", excerpt: "Những người có nhóm máu hiếm luôn cần sự chung tay đặc biệt từ cộng đồng. Bạn có thể là một trong số họ." },
  { id: 6, title: "Hành trình giọt máu: Từ người hiến đến bệnh nhân", category: "Quy trình", image: "https://images.unsplash.com/photo-1518152006812-edab29b069ac?q=80&w=2940&auto=format&fit=crop", author: "Kỹ thuật viên Lan", date: "02 Tháng 7, 2024", excerpt: "Khám phá quy trình xử lý và bảo quản nghiêm ngặt để đảm bảo mỗi đơn vị máu đều an toàn và chất lượng." },
];

const PostCard = ({ post }) => (
  <div className="bg-white rounded-xl shadow-lg overflow-hidden group transform hover:-translate-y-2 transition-transform duration-300">
    <Link to={`/blog/${post.id}`}>
      <div className="relative">
        <img src={post.image} alt={post.title} className="w-full h-56 object-cover group-hover:scale-105 transition-transform duration-300" />
        <div className="absolute top-4 left-4 bg-red-600 text-white text-xs font-bold px-3 py-1 rounded-full">{post.category}</div>
      </div>
      <div className="p-6">
        <h3 className="text-xl font-bold text-slate-800 mb-3 group-hover:text-red-600 transition-colors">{post.title}</h3>
        <p className="text-slate-500 leading-relaxed mb-4">{post.excerpt}</p>
        <div className="flex items-center justify-between text-sm text-slate-400">
           <div className="flex items-center">
             <FaUser className="mr-2" /> <span>{post.author}</span>
           </div>
           <div className="flex items-center">
             <FaCalendarAlt className="mr-2" /> <span>{post.date}</span>
           </div>
        </div>
      </div>
    </Link>
  </div>
);

const Pagination = ({ postsPerPage, totalPosts, paginate, currentPage }) => {
  const pageNumbers = [];
  for (let i = 1; i <= Math.ceil(totalPosts / postsPerPage); i++) {
    pageNumbers.push(i);
  }

  return (
    <nav className="mt-16 flex justify-center">
      <ul className="flex items-center -space-x-px h-10 text-base">
        {pageNumbers.map(number => (
          <li key={number}>
            <button
              onClick={() => paginate(number)}
              className={`flex items-center justify-center px-4 h-10 leading-tight border transition-colors duration-200 ${
                currentPage === number
                  ? 'bg-red-600 border-red-600 text-white'
                  : 'bg-white border-gray-300 text-gray-500 hover:bg-red-100 hover:text-red-700'
              } ${number === 1 ? 'rounded-l-lg' : ''} ${number === pageNumbers.length ? 'rounded-r-lg' : ''}`}
            >
              {number}
            </button>
          </li>
        ))}
      </ul>
    </nav>
  );
};

const BlogPage = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const [postsPerPage] = useState(6); // Số bài viết trên mỗi trang

  // Logic để lấy các bài viết cho trang hiện tại
  const indexOfLastPost = currentPage * postsPerPage;
  const indexOfFirstPost = indexOfLastPost - postsPerPage;
  const currentPosts = blogPosts.slice(indexOfFirstPost, indexOfLastPost);

  // Hàm thay đổi trang
  const paginate = (pageNumber) => setCurrentPage(pageNumber);

  return (
    <div className="bg-slate-100">
      {/* Hero Section */}
      <section className="bg-gradient-to-r from-red-700 to-pink-600 text-white py-20">
        <div className="container mx-auto px-6 text-center">
          <h1 className="text-5xl font-extrabold tracking-tight">Blog & Tin Tức</h1>
          <p className="mt-4 text-lg text-pink-200">Cập nhật những kiến thức, câu chuyện và sự kiện mới nhất từ cộng đồng.</p>
        </div>
      </section>

      {/* Blog Grid Section */}
      <section className="py-24">
        <div className="container mx-auto px-6">
          <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-10">
            {currentPosts.map(post => (
              <PostCard key={post.id} post={post} />
            ))}
          </div>
          
          {/* Pagination Component */}
          {blogPosts.length > postsPerPage && (
            <Pagination 
              postsPerPage={postsPerPage}
              totalPosts={blogPosts.length}
              paginate={paginate}
              currentPage={currentPage}
            />
          )}
        </div>
      </section>
    </div>
  );
};

export default BlogPage;