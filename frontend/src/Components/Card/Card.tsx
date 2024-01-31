//tsrafce
import React from "react";

type Props = {
  companyName: string;
  ticker: string;
  price: number;
};

const Card: React.FC<Props> = ({
  companyName,
  ticker,
  price,
}: Props): JSX.Element => {
  return (
    <div className="Card">
      <img src="logo192.png" alt="image" />
      <div className="details">
        <h2>
          {companyName} ({ticker})
        </h2>
        <p>{price}$</p>
      </div>
      <p className="infon">Testing this classname out for the craic</p>
    </div>
  );
};

export default Card;
