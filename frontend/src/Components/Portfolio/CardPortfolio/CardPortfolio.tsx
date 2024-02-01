import React, { SyntheticEvent } from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";
import { Link } from "react-router-dom";

interface Props {
  portfolioValue: string;
  OnRemovePortfolio: (e: SyntheticEvent) => void;
}

const CardPortfolio = ({ portfolioValue, OnRemovePortfolio }: Props) => {
  return (
    <div className="flex flex-col w-full p-8 space-y-4 text-center rounded-lg shadow-lg md:w-1/3">
      <Link
        to={`/company/${portfolioValue}`}
        className="pt-6 text-xl font-bold"
      >
        {portfolioValue}
      </Link>
      <DeletePortfolio
        portfolioValue={portfolioValue}
        OnRemovePortfolio={OnRemovePortfolio}
      />
    </div>
  );
};

export default CardPortfolio;
