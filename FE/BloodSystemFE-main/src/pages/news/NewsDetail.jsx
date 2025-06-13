import React from 'react';
import {  useParams } from 'react-router-dom';
import { newsData } from './newsData';
import Header from '../../components/ui/Header';
import Footer from '../../components/ui/Footer';

const NewsDetail = () => {
  const { id } = useParams();
  const news = newsData.find((item) => item.id === parseInt(id));

  if (!news) {
    return (
      <div className="min-h-screen bg-slate-100 flex items-center justify-center text-xl font-semibold">
        Không tìm thấy tin tức
      </div>
    );
  }

  return (
    <div className="bg-slate-50 text-slate-800">

      <main className="min-h-screen py-12 px-4">
        <div className="max-w-4xl mx-auto">

          <div className="bg-white rounded-2xl shadow-lg overflow-hidden">
            <img
              src={news.image}
              alt={news.title}
              className="w-full h-96 object-cover"
            />

            <div className="p-8">
              <h1 className="text-4xl font-bold mb-4 leading-snug text-slate-800">
                {news.title}
              </h1>

              <div className="flex items-center text-sm text-slate-500 mb-6">
                <span>{news.date}</span>
                <span className="mx-2">•</span>
                <span>{news.readTime} phút đọc</span>
              </div>

              <p className="text-lg leading-relaxed text-slate-600 whitespace-pre-line">
                {news.content}
              </p>
            </div>
          </div>
        </div>
      </main>

    </div>
  );
};

export default NewsDetail;
