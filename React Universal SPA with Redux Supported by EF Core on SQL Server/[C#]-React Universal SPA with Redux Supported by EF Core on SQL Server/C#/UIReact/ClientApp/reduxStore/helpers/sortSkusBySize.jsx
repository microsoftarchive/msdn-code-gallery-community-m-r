const sortSkusBySize = (skus) => {
    if (skus[0].size.includes('cm')) {
        skus.sort((a, b) => {
            const aSize = Number((a.size.split('cm'))[0]);
            const bSize = Number((b.size.split('cm'))[0]);

            return aSize - bSize;
        });

        return skus;
    } else {
        const scorer = {
            'X-Small': 0,
            'Small': 1,
            'Medium': 2,
            'Large': 3,
            'X-Large': 4
        }

        skus.sort((a, b) => {
            return scorer[a.size] - scorer[b.size];
        });

        return skus;
    }
}

export default sortSkusBySize;