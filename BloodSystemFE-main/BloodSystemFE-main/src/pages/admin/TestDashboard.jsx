import React from 'react';

const TestDashboard = () => {
  return (
    <div className="min-h-screen bg-slate-50 p-8">
      <div className="max-w-7xl mx-auto">
        <h1 className="text-4xl font-bold text-slate-800 mb-8">
          Test Dashboard
        </h1>
        
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {/* Test Card 1 */}
          <div className="bg-gradient-to-br from-red-500 to-red-600 rounded-2xl shadow-xl overflow-hidden">
            <div className="p-8 text-white">
              <div className="text-5xl font-bold mb-4">6</div>
              <div className="text-lg font-semibold">LISTED BLOOD GROUPS</div>
            </div>
            <div className="bg-white bg-opacity-20 px-8 py-4">
              <div className="flex items-center justify-between text-white text-sm font-medium">
                <span>FULL DETAIL</span>
                <span>→</span>
              </div>
            </div>
          </div>

          {/* Test Card 2 */}
          <div className="bg-gradient-to-br from-pink-500 to-pink-600 rounded-2xl shadow-xl overflow-hidden">
            <div className="p-8 text-white">
              <div className="text-5xl font-bold mb-4">9</div>
              <div className="text-lg font-semibold">REGISTERED BLOOD GROUP</div>
            </div>
            <div className="bg-white bg-opacity-20 px-8 py-4">
              <div className="flex items-center justify-between text-white text-sm font-medium">
                <span>FULL DETAIL</span>
                <span>→</span>
              </div>
            </div>
          </div>

          {/* Test Card 3 */}
          <div className="bg-gradient-to-br from-red-400 to-pink-500 rounded-2xl shadow-xl overflow-hidden">
            <div className="p-8 text-white">
              <div className="text-5xl font-bold mb-4">0</div>
              <div className="text-lg font-semibold">TOTAL QUERIES</div>
            </div>
            <div className="bg-white bg-opacity-20 px-8 py-4">
              <div className="flex items-center justify-between text-white text-sm font-medium">
                <span>FULL DETAIL</span>
                <span>→</span>
              </div>
            </div>
          </div>

          {/* Test Card 4 */}
          <div className="bg-gradient-to-br from-pink-400 to-red-500 rounded-2xl shadow-xl overflow-hidden">
            <div className="p-8 text-white">
              <div className="text-5xl font-bold mb-4">5</div>
              <div className="text-lg font-semibold">TOTAL BLOOD REQUEST</div>
            </div>
            <div className="bg-white bg-opacity-20 px-8 py-4">
              <div className="flex items-center justify-between text-white text-sm font-medium">
                <span>FULL DETAIL</span>
                <span>→</span>
              </div>
            </div>
          </div>
        </div>

        <div className="mt-12 bg-white rounded-2xl shadow-xl border border-slate-200 p-8">
          <h3 className="text-2xl font-bold text-slate-800 mb-6">
            Test Dashboard Working!
          </h3>
          <p className="text-slate-600">
            If you can see this, the routing is working correctly.
          </p>
        </div>
      </div>
    </div>
  );
};

export default TestDashboard;
