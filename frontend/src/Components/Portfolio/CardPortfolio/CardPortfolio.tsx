import React, { SyntheticEvent } from "react";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";

interface Props {
  portfolioValue: string;
  OnRemovePortfolio: (e: SyntheticEvent) => void;
}

const CardPortfolio = ({ portfolioValue, OnRemovePortfolio }: Props) => {
  return (
    <>
      <h4>{portfolioValue}</h4>
      <DeletePortfolio
        OnRemovePortfolio={OnRemovePortfolio}
        portfolioValue={portfolioValue}
      />
    </>
  );
};

export default CardPortfolio;
