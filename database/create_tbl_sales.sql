CREATE TABLE IF NOT EXISTS `tbl_sales` (
  `sale_id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `sale_date` DATETIME NOT NULL,
  `total_amount` DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  `user_id` INT NULL,
  `payment_method` VARCHAR(50) NULL,
  `notes` TEXT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  INDEX `idx_sale_date` (`sale_date`),
  INDEX `idx_user_id` (`user_id`),
  CONSTRAINT `fk_sales_user` FOREIGN KEY (`user_id`) REFERENCES `tbl_users`(`user_id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;