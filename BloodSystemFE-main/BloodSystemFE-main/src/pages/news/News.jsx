import React from "react";
import { useNavigate } from "react-router-dom";
import Header from "../../components/ui/Header";
import { FaFacebook, FaTwitter, FaInstagram, FaLinkedin } from "react-icons/fa";
import { newsData } from "./NewsData";
import Footer from "../../components/ui/Footer";

const NewsPage = () => {
  const navigate = useNavigate();

  return (
    <div className="bg-slate-50 text-slate-800">

      <section className="py-24 px-4">
        <div className="max-w-7xl mx-auto">
          <div className="text-center mb-16">
            <h2 className="text-4xl font-bold mb-4">Latest News & Updates</h2>
            <p className="text-lg text-slate-500">Stay informed with the newest stories and announcements.</p>
          </div>

          <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
            {newsData.map((news) => (
              <div 
                key={news.id} 
                className="bg-white rounded-2xl overflow-hidden shadow-md hover:shadow-xl transform hover:-translate-y-1 transition-all duration-300 cursor-pointer"
                onClick={() => navigate(`/news/${news.id}`)}
              >
                <img src={news.image} alt={news.title} className="w-full h-56 object-cover" />
                <div className="p-6">
                  <h3 className="text-xl font-bold text-slate-800 mb-2">{news.title}</h3>
                  <p className="text-slate-500 text-sm mb-3 line-clamp-3">{news.excerpt}</p>
                  <div className="flex justify-between items-center text-sm text-slate-400">
                    <span>{news.date}</span>
                    <span>{news.readTime} min read</span>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>
    </div>
  );
};

export default NewsPage;
