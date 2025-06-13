import React, { useState } from "react";
import { FaHeart, FaTint, FaInfoCircle, FaHospital, FaSearch, FaFilter, FaPlay } from "react-icons/fa";

const BloodTypePage = () => {
  const [selectedType, setSelectedType] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [filterType, setFilterType] = useState("all");

  const bloodTypes = [
    {
      type: "A+",
      description: "Second most common blood type",
      percentage: "35.7%",
      canDonateTo: ["A+", "AB+"],
      canReceiveFrom: ["A+", "A-", "O+", "O-"]
    },
    {
      type: "A-",
      description: "Universal plasma donor",
      percentage: "6.3%",
      canDonateTo: ["A+", "A-", "AB+", "AB-"],
      canReceiveFrom: ["A-", "O-"]
    },
    {
      type: "B+",
      description: "Third most common blood type",
      percentage: "8.5%",
      canDonateTo: ["B+", "AB+"],
      canReceiveFrom: ["B+", "B-", "O+", "O-"]
    },
    {
      type: "B-",
      description: "Rare blood type",
      percentage: "1.5%",
      canDonateTo: ["B+", "B-", "AB+", "AB-"],
      canReceiveFrom: ["B-", "O-"]
    },
    {
      type: "AB+",
      description: "Universal recipient",
      percentage: "3.4%",
      canDonateTo: ["AB+"],
      canReceiveFrom: ["All Types"]
    },
    {
      type: "AB-",
      description: "Rarest blood type",
      percentage: "0.6%",
      canDonateTo: ["AB+", "AB-"],
      canReceiveFrom: ["A-", "B-", "O-", "AB-"]
    },
    {
      type: "O+",
      description: "Most common blood type",
      percentage: "37.4%",
      canDonateTo: ["O+", "A+", "B+", "AB+"],
      canReceiveFrom: ["O+", "O-"]
    },
    {
      type: "O-",
      description: "Universal donor",
      percentage: "6.6%",
      canDonateTo: ["All Types"],
      canReceiveFrom: ["O-"]
    }
  ];

  const facts = [
    "Someone needs blood every two seconds",
    "One donation can save up to three lives",
    "Less than 38% of the population is eligible to donate blood",
    "Blood cannot be manufactured – it can only come from donors"
  ];

  const filteredBloodTypes = bloodTypes.filter(blood => {
    const matchesSearch = blood.type.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         blood.description.toLowerCase().includes(searchTerm.toLowerCase());
    if (filterType === "all") return matchesSearch;
    return matchesSearch && blood.type.includes(filterType);
  });

  return (
    <div className="min-h-screen bg-gradient-to-r from-red-500 to-pink-600 py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-7xl mx-auto">
        <div className="text-center mb-16">
          <h1 className="text-4xl md:text-5xl font-bold text-white mb-4 drop-shadow-lg">
            Understanding Blood Types
          </h1>
          <p className="text-xl text-white opacity-90">
            Learn about different blood types and their compatibility
          </p>
        </div>

        <div className="mb-8 flex flex-col md:flex-row gap-4">
          <div className="relative flex-1">
            <FaSearch className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400" />
            <input
              type="text"
              placeholder="Search blood types..."
              className="w-full pl-10 pr-4 py-2 rounded-lg border border-gray-300 bg-white text-gray-800 focus:outline-none focus:ring-2 focus:ring-red-500"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>
          <select
            className="bg-white bg-white bold px-4 py-2 rounded-lg border focus:outline-none focus:ring-2 focus:ring-red-500"
            value={filterType}
            onChange={(e) => setFilterType(e.target.value)}
          >
            <option value="all">All Types</option>
            <option value="+">Positive Types</option>
            <option value="-">Negative Types</option>
          </select>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-16">
          {filteredBloodTypes.map((blood) => (
            <div
              key={blood.type}
              className="bg-white rounded-lg p-6 transform transition-all duration-300 hover:scale-105 hover:shadow-xl cursor-pointer"
              onClick={() => setSelectedType(blood)}
            >
              <div className="flex items-center justify-between mb-4">
                <span className="text-3xl font-bold text-red-600">{blood.type}</span>
                <FaTint className="text-2xl text-red-600" />
              </div>
              <p className="text-gray-600 mb-2">{blood.description}</p>
              <div className="bg-gray-100 rounded-full h-2 mb-2">
                <div
                  className="bg-red-600 rounded-full h-2"
                  style={{ width: blood.percentage }}
                ></div>
              </div>
              <p className="text-sm text-gray-500">{blood.percentage} of population</p>
            </div>
          ))}
        </div>

        {selectedType && (
          <div className="bg-white rounded-lg p-6 mb-16 animate-fade-in">
            <h2 className="text-2xl font-bold text-gray-800 mb-4">
              {selectedType.type} Compatibility
            </h2>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <h3 className="text-lg font-semibold text-gray-700 mb-2">
                  Can Donate To:
                </h3>
                <div className="flex flex-wrap gap-2">
                  {selectedType.canDonateTo.map((type) => (
                    <span
                      key={type}
                      className="bg-green-100 text-green-800 px-3 py-1 rounded-full"
                    >
                      {type}
                    </span>
                  ))}
                </div>
              </div>
              <div>
                <h3 className="text-lg font-semibold text-gray-700 mb-2">
                  Can Receive From:
                </h3>
                <div className="flex flex-wrap gap-2">
                  {selectedType.canReceiveFrom.map((type) => (
                    <span
                      key={type}
                      className="bg-blue-100 text-blue-800 px-3 py-1 rounded-full"
                    >
                      {type}
                    </span>
                  ))}
                </div>
              </div>
            </div>
          </div>
        )}

        <div className="bg-white rounded-lg p-6 mb-16">
          <h2 className="text-2xl font-bold text-gray-800 mb-6 text-center">
            Quick Facts About Blood Donation
          </h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {facts.map((fact, index) => (
              <div
                key={index}
                className="flex items-start space-x-4 p-4 bg-gray-50 rounded-lg"
              >
                <FaInfoCircle className="text-red-600 text-xl flex-shrink-0 mt-1" />
                <p className="text-gray-700">{fact}</p>
              </div>
            ))}
          </div>
        </div>       

        <div className="text-center">
          <button className="bg-white text-red-600 px-8 py-4 rounded-full font-bold text-lg shadow-lg transform transition-all duration-300 hover:scale-105 hover:shadow-xl flex items-center justify-center mx-auto">
            <FaHospital className="mr-2" />
            Find Local Donation Centers
          </button>
        </div>
      </div>
    </div>
  );
};

export default BloodTypePage;